using AppCorev1.Business.Models;
using AppCorev1.Business.Services.Bases;
using AppCorev1.DataAccess.Entityframework;
using AppCorev1.DataAccess.Entityframework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IMagazaService : IService<MagazaModel, Magaza, ETicaretContext>
    {
        Result<List<MagazaModel>> List(); //controller da ula�abilmek i�in �nterface i�inde yeni yazd���m�z metodu tan�mlad�k
    }
    public class MagazaService : IMagazaService
    {
        public RepoBase<Magaza, ETicaretContext> Repo { get; set; } = new Repo<Magaza, ETicaretContext>();

        public Result Add(MagazaModel model)
        {
            //if (Repo.Query().Any(m => m.Adi.ToLower() == model.Adi.ToLower().Trim()))
            //    return new ErrorResult("Girdi�iniz Ma�aza ad�na sahip kay�t bulunmaktad�r!");
            if (Repo.EntityExists(m => m.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdi�iniz Ma�aza ad�na sahip kay�t bulunmaktad�r!");
            Magaza entity = new Magaza()
            {
                Adi = model.Adi.Trim(),
                Puani = model.Puani, //entity de required olmad��� i�in .Value() demeye gerek kalmad�
                SanalMi = model.SanalMi,
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            //Magaza entity = Repo.Query().SingleOrDefault(m => m.Id == id);
            Magaza entity = Repo.Query(m => m.Id == id, "UrunMagazalar").SingleOrDefault(); // entity deki ili�kili tablo UrunMagazalar
            if (entity.UrunMagazalar != null && entity.UrunMagazalar.Count > 0)
            {
                RepoBase<UrunMagaza, ETicaretContext> urunMagazaRepo = new Repo<UrunMagaza, ETicaretContext>(); // sadece bu metodda kullanaca��m�z i�in burada new ledik
                foreach (var urunMagaza in entity.UrunMagazalar)
                {
                    urunMagazaRepo.Delete(urunMagaza, false);
                }
                urunMagazaRepo.Save(); //tek seferde silme i�lemi yapt�k 
                urunMagazaRepo.Dispose(); //ma�aza ile ayn� dbcontext teki db objesini kullanmad��� i�in dispose edebilirsin

            }
            Repo.Delete(entity);
            return new SuccessResult("Magaza ba�ar�yla silindi!");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<MagazaModel> Query()
        {
            return Repo.Query().OrderBy(m => m.Adi).Select(m => new MagazaModel()
            {
                Adi = m.Adi,
                Id = m.Id,
                Puani = m.Puani,
                SanalMi = m.SanalMi,
                SanalMiDisplay = m.SanalMi ? "Evet" : "Hay�r"
            });
        }

        public Result Update(MagazaModel model)
        {
            if (Repo.EntityExists(m => m.Adi.ToLower() == model.Adi.ToLower().Trim() && m.Id != model.Id))
                return new ErrorResult("Girdi�iniz Ma�aza ad�na sahip kay�t bulunmaktad�r!");
            Magaza entity = Repo.Query().SingleOrDefault(m => m.Id == model.Id);

            entity.Adi = model.Adi.Trim();
            entity.Puani = model.Puani;
            entity.SanalMi = model.SanalMi;

            Repo.Update(entity);
            return new SuccessResult();
        }
        public Result<List<MagazaModel>> List() //sorgu sonucu liste tipinde liste d�nmesi i�in metod yazd�k, burada Result veri ta��yor
        {
            var list = Query().ToList();
            if (list.Count == 0)
                return new ErrorResult<List<MagazaModel>>("Ma�aza bulunamad�!", list); // burada liste bo� gelirse hata almas�n diye bo� liste g�nderdik, List yazarak
            return new SuccessResult<List<MagazaModel>>(list.Count + " adet ma�aza bulundu", list);
        }
    }
}
