using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnviarEmail.Models
{
    public class Destinatario
    {
        public string Email { get; set; }

        public Destinatario(string email)
        {
            this.Email = email;
        }
    }
}
