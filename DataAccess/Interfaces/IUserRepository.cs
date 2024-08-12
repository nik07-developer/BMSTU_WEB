using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.DTO;
using DataAccess.DTO.User;
using Models;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Guid Create(CreateUserDTO user);
        public UserDTO Get(Guid id);
        public void Update(Guid id, UpdateUserDTO update);
        public void Delete(Guid id);

        public UserDTO Find(string login, string password);
    }
}
