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
        public void Update(Guid id, UpdateUserDTO user);
        public void Delete(Guid id);
    }
}
