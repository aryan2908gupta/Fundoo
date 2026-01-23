using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Dto
{
    public class ForgetPasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
