using DataAccess.DTO;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class UserRepositoryPlaceholder : IUserRepository
	{
		List<Guid> guids = new();

		public Guid Create(CreateUserDTO user)
		{
			guids.Append(Guid.NewGuid());
			return guids.Last();
		}

		public void Delete(Guid id)
		{
			guids.Remove(id);
		}

		public UserDTO Get(Guid id)
		{
			return new UserDTO(id, "login", "passowrd", "name");
		}

		public void UpdateName(Guid id, string name)
		{

		}

        public void UpdatePassword(Guid id, string password)
        {

        }
    }
}
