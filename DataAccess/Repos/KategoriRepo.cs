using DataAccess.Contexts;
using DataAccess.Repos.Bases;

namespace DataAccess.Repos
{
    public class KategoriRepo : KategoriRepoBase
    {
        public KategoriRepo() : base()
        {

        }
        public KategoriRepo(ETicaretContext dbContext) : base(dbContext)
        {

        }
    }
}
