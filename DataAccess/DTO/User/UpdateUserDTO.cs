using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.User
{
    public class UpdateUserDTO
    {
        public string? Password;
        public string? Name;

        public UpdateUserDTO(string? password, string? name)
        {
            Password = password;
            Name = name;
        }
    }
}
