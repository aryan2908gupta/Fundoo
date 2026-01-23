using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email);
    }
}
