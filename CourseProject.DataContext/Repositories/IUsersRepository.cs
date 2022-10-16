using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface IUsersRepository
    {
        Task<AuthResult> DeleteUser(string userToDelete);
        Task<AuthResult> BanUnbanUser(string userToBanUnban);
        Task<AuthResult> ChangeRoleUser(string userToChangeRole);
    }
}
