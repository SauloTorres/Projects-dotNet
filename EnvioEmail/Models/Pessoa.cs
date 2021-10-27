using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnvioEmail.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        public string Nome  { get; set; }

        [Required]
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;
    }
}