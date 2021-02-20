using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviewRecommend.BLL;
using MoviewRecommend.Service.Interfaces;
using MoviewRecommend.Service.Services;
using MoviewRecommend.Service.Tasks;

namespace MoviewRecommend.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


            services.AddCors();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //JWT kullanacağım ve ayarları da şunlar olsun dediğimiz yer ise burasıdır.
                .AddJwtBearer(x =>
                {
                    //Gelen isteklerin sadece HTTPS yani SSL sertifikası olanları kabul etmesi(varsayılan true)
                    x.RequireHttpsMetadata = false;
                    //Eğer token onaylanmış ise sunucu tarafında kayıt edilir.
                    x.SaveToken = true;
                    //Token içinde neleri kontrol edeceğimizin ayarları.
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Token 3.kısım(imza) kontrolü
                        ValidateIssuerSigningKey = true,
                        //Neyle kontrol etmesi gerektigi
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            Service.Infrastructure.DependencyRegistrar.Registrar(services, Configuration.GetConnectionString("Mssql-Dev"));

            services.AddCronJob<GetTheMovieAllScheduleTask>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"*/5 * * * *";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

        }
    }
}
