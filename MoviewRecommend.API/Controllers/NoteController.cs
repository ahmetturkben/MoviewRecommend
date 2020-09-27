using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviewRecommend.API.Models;
using MoviewRecommend.BLL;
using MoviewRecommend.Service.Interfaces;

namespace MoviewRecommend.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private IMovieService _movieService;
        private readonly INoteService _noteService;

        public NoteController(
            IMovieService movieService,
            INoteService noteService)
        {
            _movieService = movieService;
            _noteService = noteService;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] NoteModel model)
        {
            if (ModelState.IsValid)
            {
                int? movieId = _movieService.GetSingle(x => x.Id == model.MovieId)?.Id;
                if (movieId == 0 || movieId == null)
                    return new ApiResponse { IsSuccess = false, Message = "Film bulunamadı!" };

                BLL.Note note = new Note
                {
                    MovieId = model.MovieId,
                    Notes = model.Note,
                    Rate = model.Rate
                };

                _noteService.Add(note);
                _noteService.SaveChanges();
                return new ApiResponse { IsSuccess = true, Message = "İşlem başarılı" };
            }

            return new ApiResponse { IsSuccess = false, Message = "Validasyon hatası" };
        }
    }
}
