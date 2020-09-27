using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviewRecommend.BLL;
using MoviewRecommend.DAL.Interfaces;
using MoviewRecommend.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviewRecommend.Service.Services
{
    public class UserService : ServiceBase<DAL.Entities.User, BLL.User>, IUserService
    {
        private readonly AppSettings _appSettings;
        public UserService(IMapper mapper, IUserRepository userRepository, IOptions<AppSettings> appSettings) : base(mapper)
        {
            _appSettings = appSettings.Value;
            base.InitializeBase(userRepository);
        }

        public (string username, string token)? Authenticate(string username, string password)
        {
            var user = GetSingle(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Özel olarak şu Claimler olsun dersek buraya ekleyebiliriz.
                Subject = new ClaimsIdentity(new[]
                {
                    //İstersek string bir property istersek ClaimsTypes sınıfının sabitlerinden çağırabiliriz.
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                //Tokenın hangi tarihe kadar geçerli olacağını ayarlıyoruz.
                Expires = DateTime.UtcNow.AddMinutes(2),
                //Son olarak imza için gerekli algoritma ve gizli anahtar bilgisini belirliyoruz.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Token oluşturuyoruz.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Oluşturduğumuz tokenı string olarak bir değişkene atıyoruz.
            string generatedToken = tokenHandler.WriteToken(token);

            user.Token = "Bearer " + generatedToken;
            Update(user);
            SaveChanges();

            //Sonuçlarımızı tuple olarak dönüyoruz.
            return (user.Username, generatedToken);
        }
    }
}
