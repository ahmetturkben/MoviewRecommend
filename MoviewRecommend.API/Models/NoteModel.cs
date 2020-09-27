using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviewRecommend.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MoviewRecommend.API.Models
{
    public class NoteModel
    {

        [Required]
        [Range(1,10, ErrorMessage = "1 ile 10 arasında bir puan veriniz.")]
        public int Rate { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
}
