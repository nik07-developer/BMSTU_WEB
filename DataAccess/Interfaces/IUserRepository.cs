using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.DTO;
using Models;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Guid Create(CreateUserDTO user);
        public UserDTO Get(Guid id);
        public void UpdateName(Guid id, string name);
        public void UpdatePassword(Guid id, string password);
        public void Delete(Guid id);

        public UserDTO Find(string login, string password);
    }
}
