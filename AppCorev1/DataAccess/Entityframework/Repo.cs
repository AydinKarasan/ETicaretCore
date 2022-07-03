using AppCorev1.DataAccess.Entityframework.Bases;
using Microsoft.EntityFrameworkCore;

namespace AppCorev1.DataAccess.Entityframework
{
    public class Repo<TEntity, TDbContext> : RepoBase<TEntity, TDbContext> where TEntity : class, new() where TDbContext : DbContext, new()
    {
        public Repo() : base()
        {
            
        }
        public Repo(TDbContext dbContext) : base (dbContext)
        {
            
        }
    }
}
