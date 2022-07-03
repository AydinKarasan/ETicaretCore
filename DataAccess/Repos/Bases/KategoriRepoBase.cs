using AppCorev1.DataAccess.Entityframework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repos.Bases
{
    public abstract class KategoriRepoBase : RepoBase <Kategori, ETicaretContext >
    {
        protected KategoriRepoBase() : base()
        {

        }
        protected KategoriRepoBase(ETicaretContext dbContext) : base(dbContext)
        {

        }
    }
}
