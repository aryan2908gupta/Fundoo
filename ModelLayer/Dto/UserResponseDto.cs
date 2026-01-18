using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Dto
{
 public class UserResponseDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string token { get; set; }
    }
}
