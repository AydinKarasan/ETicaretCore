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
        Result<List<MagazaModel>> List(); //controller da ulaþabilmek için ýnterface içinde yeni yazdýðýmýz metodu tanýmladýk
    }
    public class MagazaService : IMagazaService
    {
        public RepoBase<Magaza, ETicaretContext> Repo { get; set; } = new Repo<Magaza, ETicaretContext>();

        public Result Add(MagazaModel model)
        {
            //if (Repo.Query().Any(m => m.Adi.ToLower() == model.Adi.ToLower().Trim()))
            //    return new ErrorResult("Girdiðiniz Maðaza adýna sahip kayýt bulunmaktadýr!");
            if (Repo.EntityExists(m => m.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdiðiniz Maðaza adýna sahip kayýt bulunmaktadýr!");
            Magaza entity = new Magaza()
            {
                Adi = model.Adi.Trim(),
                Puani = model.Puani, //entity de required olmadýðý için .Value() demeye gerek kalmadý
                SanalMi = model.SanalMi,
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            //Magaza entity = Repo.Query().SingleOrDefault(m => m.Id == id);
            Magaza entity = Repo.Query(m => m.Id == id, "UrunMagazalar").SingleOrDefault(); // entity deki iliþkili tablo UrunMagazalar
            if (entity.UrunMagazalar != null && entity.UrunMagazalar.Count > 0)
            {
                RepoBase<UrunMagaza, ETicaretContext> urunMagazaRepo = new Repo<UrunMagaza, ETicaretContext>(); // sadece bu metodda kullanacaðýmýz için burada new ledik
                foreach (var urunMagaza in entity.UrunMagazalar)
                {
                    urunMagazaRepo.Delete(urunMagaza, false);
                }
                urunMagazaRepo.Save(); //tek seferde silme iþlemi yaptýk 
                urunMagazaRepo.Dispose(); //maðaza ile ayný dbcontext teki db objesini kullanmadýðý için dispose edebilirsin

            }
            Repo.Delete(entity);
            return new SuccessResult("Magaza baþarýyla silindi!");
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
                SanalMiDisplay = m.SanalMi ? "Evet" : "Hayýr"
            });
        }

        public Result Update(MagazaModel model)
        {
            if (Repo.EntityExists(m => m.Adi.ToLower() == model.Adi.ToLower().Trim() && m.Id != model.Id))
                return new ErrorResult("Girdiðiniz Maðaza adýna sahip kayýt bulunmaktadýr!");
            Magaza entity = Repo.Query().SingleOrDefault(m => m.Id == model.Id);

            entity.Adi = model.Adi.Trim();
            entity.Puani = model.Puani;
            entity.SanalMi = model.SanalMi;

            Repo.Update(entity);
            return new SuccessResult();
        }
        public Result<List<MagazaModel>> List() //sorgu sonucu liste tipinde liste dönmesi için metod yazdýk, burada Result veri taþýyor
        {
            var list = Query().ToList();
            if (list.Count == 0)
                return new ErrorResult<List<MagazaModel>>("Maðaza bulunamadý!", list); // burada liste boþ gelirse hata almasýn diye boþ liste gönderdik, List yazarak
            return new SuccessResult<List<MagazaModel>>(list.Count + " adet maðaza bulundu", list);
        }
    }
}
