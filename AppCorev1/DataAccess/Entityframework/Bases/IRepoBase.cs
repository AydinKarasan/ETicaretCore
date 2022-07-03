using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCorev1.DataAccess.Entityframework.Bases
{
    //TEntity:class, new() diyerek sadece new leyebildiðin referanslar gelmesi için bunu yazdýk (yani sadece entity ler gelsin diye) 
    //dbContext içinde aynýsýný yapýyoruz//class yerine de dbContext yazdýk ki dbContext den gelen context class larý kullanalým                                                                
    // generic bir yapý oluþturduk ve hangi tip entity i gönderirsek çalýþýr // IRepoBase<Ogrenci> gibi
    // en son IDisposable ekledik ki kullanýlmayan objeleri garbage collector silsin diye yaptýk
    public interface IRepoBase<TEntity, TDbContext> : IDisposable where TEntity:class,new() where TDbContext:DbContext,new()
    {
        TDbContext DbContext { get; set; }
        IQueryable<TEntity> Query(params string[] entitiesToInclude);//Read  // select sorgusu için taným // select * from 
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude); //üstteki metodu overload ettik
        
        void Add(TEntity entity, bool save = true); // Create
        void Update(TEntity entity, bool save = true); // Update
        void Delete(TEntity entity, bool save = true); // Delete
        void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true);
        int Save();
    }
}
