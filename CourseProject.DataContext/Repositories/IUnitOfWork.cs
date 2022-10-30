using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository Account { get; }
        IItemsRepository Items { get; }
        ITagsRepository Tags { get; }
        IUsersRepository Users { get; }
        ICollectionsRepository Collections { get; }
        ICommentsRepository Comments { get; }
        void Save();
    }
}
