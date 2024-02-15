using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Business.Dtos
{
    public class SignInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
