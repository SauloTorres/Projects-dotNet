using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnvioEmail.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tarefa")]
        public string Title { get; set; }
        [Display(Name = "Feito")]
        public bool Done { get; set; }
    }
}