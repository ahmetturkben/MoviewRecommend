using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MoviewRecommend.API.Models;
using MoviewRecommend.BLL;
using MoviewRecommend.Service.Interfaces;

namespace MoviewRecommend.API.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieService _movieService;
        private IUserService _userService;
        private readonly INoteService _noteService;
        private readonly IEmailService _emailService;

        public MovieController(
            IMovieService movieService,
            IUserService userService,
            INoteService noteService,
            IEmailService emailService)
        {
            _movieService = movieService;
            _userService = userService;
            _noteService = noteService;
            _emailService = emailService;
        }

        [HttpGet]
        public ApiResponse Get(int page = 0, int pageSize = 20)
        {
            return new ApiResponse { IsSuccess = true, Object = _movieService.GetAll().Skip(page * pageSize).Take(pageSize) };
        }

        [HttpGet]
        public ApiResponse GetDetail(int movieId = 0)
        {
            if (movieId == 0)
                return new ApiResponse { IsSuccess = false, Message = "MovieId 0 olamaz." };

            var movie = _movieService.GetSingle(x => x.Id == movieId);
            if (movie == null)
                return new ApiResponse { IsSuccess = false, Message = "Film bulunamadı." };

            var token = HttpContext.Request.Headers["Authorization"][0];
            var user = _userService.GetSingle(x => x.Token == token)?.Id;
            var note = _noteService.GetSingle(x => x.MovieId == movie.Id && x.UserId == user);

            Models.MovieDetails movieDetail = new MovieDetails
            {
                AveragateRate = movie.AverageScore,
                Description = movie.Description,
                Note = note?.Notes,
                Rate = note == null ? 0 : note.Rate,
                Title = movie?.Name
            };

            return new ApiResponse { IsSuccess = true, Object = movieDetail };
        }

        [HttpPost]
        public ApiResponse SendMail(string email = "")
        {
            if (_emailService.SendMail(email))
                return new ApiResponse { IsSuccess = true, Object = null, Message = "Mail gönderildi." };
            return new ApiResponse { IsSuccess = false, Object = null, Message = "Mail gönderilemedi." };
        }
    }
}
