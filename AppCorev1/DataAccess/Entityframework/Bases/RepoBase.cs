using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCorev1.DataAccess.Entityframework.Bases
{
    public abstract class RepoBase<TEntity, TDbContext> : IRepoBase<TEntity, TDbContext> where TEntity : class, new() where TDbContext : DbContext, new()
    {
        public TDbContext DbContext { get; set; }
        protected RepoBase()
        {
            DbContext = new TDbContext();
        }
        protected RepoBase(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Add(TEntity entity, bool save = true)
        {
            DbContext.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }
        public int Save()
        {
            try
            { 
                return DbContext.SaveChanges(); 
            }
            catch (Exception exc)
            {
                throw exc;
                //exc.InnerException.Message // entityframework hatalara inner exception üzerinden ulaþýr //hatalarý log lamak için kullanýlýr
            }
        }

        public void Delete(TEntity entity, bool save = true)
        {
            DbContext.Set<TEntity>().Remove(entity);
            if (save)
                Save();
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = Query(predicate).ToList();
            foreach (var entity in entities)
            {
                Delete(entity, false);
            }
            if (save)
                Save();
         }

        public void Dispose()
        {
            DbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
        // var repo = new Repo<Konu, KonuYorumContext>();
        // var list = repo.Query().ToList();
        // list = repo.Query("Yorum").ToList();
        // list = repo.Query(konu => konu.Baslik == "Dolar").ToList();
        // list = repo.Query(konu => konu.Baslik == "Dolar", "Yorum").ToList();
        public IQueryable<TEntity> Query(params string[] entitiesToInclude)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }
            return query;
        }

        public void Update(TEntity entity, bool save = true)
        {
            DbContext.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            var query = Query(entitiesToInclude);
            query = query.Where(predicate);
            return query;
        }

        //Repo.EntityExists(m => m.Adi =="Getir");
        public bool EntityExists(Expression<Func<TEntity, bool>>predicate)
        {
            return Query().Any(predicate);
        }
    }
}
