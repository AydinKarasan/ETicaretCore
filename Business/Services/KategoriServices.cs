using AppCorev1.Business.Models;
using AppCorev1.DataAccess.Entityframework;
using AppCorev1.DataAccess.Entityframework.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public class KategoriServices : IKategoriService
    {
        public RepoBase<Kategori, ETicaretContext> Repo { get; set; } = new Repo<Kategori, ETicaretContext>();

        //public KategoriServices(KategoriRepoBase kategoriRepo)
        //{
        //    Repo = kategoriRepo;
        //}

        public Result Add(KategoriModel model)
        {
            //Kategori mevcutKategori = Repo.Query().SingleOrDefault(x => x.Adi.ToLower() == model.Adi.ToLower().Trim());
            //if (mevcutKategori != null)
            //    return new ErrorResult("Girmek istedi�iniz kategori ad�na sahip kay�t bulunmaktad�r.");

            //Kategori mevcutKategori = Repo.Query(x => x.Adi.ToLower() == model.Adi.ToLower().Trim()).SingleOrDefault(); // predicate parametresi alan Query i�in
            //if (mevcutKategori != null)
            //    return new ErrorResult("Girmek istedi�iniz kategori ad�na sahip kay�t bulunmaktad�r.");

            if (Repo.Query().Any(x => x.Adi.ToLower() == model.Adi.ToLower().Trim())) 
                return new ErrorResult("Girmek istedi�iniz kategori ad�na sahip kay�t bulunmaktad�r.");

            Kategori entity = new Kategori() 
            {
                //Aciklamasi=string.IsNullOrWhiteSpace(model.Aciklamasi) ? null : model.Aciklamasi.Trim(),
                Aciklamasi=model.Aciklamasi?.Trim(),
                Adi=model.Adi.Trim()
            };
            Repo.Add(entity);
            return new SuccessResult("��lem ba�ar�l�.");
        }

        public Result Delete(int id)
        {
           Kategori kategori = Repo.Query(k => k.Id == id, "Urunler").SingleOrDefault();
            if (kategori.Urunler != null && kategori.Urunler.Count > 0)
            {
                return new ErrorResult("Kategori silinemez ��nk� ili�kili �r�n kay�tlar� bulunmaktad�r!");
            }
            Repo.Delete(kategori);
            //Repo.Delete(k => k.Id == id);
            return new SuccessResult("Kategori ba�ar�yla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }
        //select Adi, Aciklamasi, Id from Kategoriler order by Adi
        public IQueryable<KategoriModel> Query()
        {
            return Repo.Query("Urunler").OrderBy(kategori => kategori.Adi).Select(k => new KategoriModel()
            {
                Aciklamasi = k.Aciklamasi, //AutoMapper ile otomatik yapabilirsin incele
                Adi = k.Adi,
                Id = k.Id,
                UrunSayisiDisplay = k.Urunler.Count
            });
        }

        public Result Update(KategoriModel model)
        {
            if (Repo.Query().Any(x => x.Adi.ToLower() == model.Adi.ToLower().Trim() && x.Id != model.Id))
                return new ErrorResult("Girmek istedi�iniz kategori ad�na sahip kay�t bulunmaktad�r.");

            Kategori entity = Repo.Query(k => k.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            Repo.Update(entity);
            return new SuccessResult();
        }
    }
}
