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
        Task<List<ApplicationUser>> GetAll();
        Task<List<ApplicationUser>> GetParticularUser(string id);
        Task<AuthResult> DeleteUser(string userToDelete);
        Task<AuthResult> BanUser(string userToBan);
        Task<AuthResult> UnbanUser(string userToUnban);
        Task<AuthResult> ChangeRoleUser(string userToChangeRole);
    }
}
