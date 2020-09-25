using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviewRecommend.API.Models;
using MoviewRecommend.Service.Interfaces;

namespace MoviewRecommend.API.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] Authenticate authenticateModel)
        {
            var user = _userService.Authenticate(authenticateModel.Username, authenticateModel.Password);

            if (user == null)
                return BadRequest("Username or password incorrect!");

            return Ok(new { Username = user.Value.username, Token = user.Value.token });
        }
    }
}
