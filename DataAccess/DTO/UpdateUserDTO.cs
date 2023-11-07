using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UpdateUserDTO
    {
        public string Password { get; private set; }
        public bool IsPasswordUpdated { get; private set; }

        public string Name { get; private set; }
        public bool IsNameUpdated { get; private set; }

        public UpdateUserDTO SetPassword(string password)
        {
            Password = password;
            IsPasswordUpdated = true;
            return this;
        }

        public UpdateUserDTO SetName(string name)
        {
            Name = name;
            IsNameUpdated = true;
            return this;
        }
    }
}
