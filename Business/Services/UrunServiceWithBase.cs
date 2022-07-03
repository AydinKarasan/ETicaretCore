using AppCorev1.Business.Models;
using AppCorev1.Business.Services.Bases;
using AppCorev1.DataAccess.Entityframework;
using AppCorev1.DataAccess.Entityframework.Bases;
using AppCorev1.Utils;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IUrunService : IService<UrunModel, Urun, ETicaretContext >
    {
        void DeleteImage(int id);
    }
    public class UrunService : IUrunService
    {
        private readonly ETicaretContext _db;
        public RepoBase<Urun, ETicaretContext> Repo { get; set; } = new Repo<Urun, ETicaretContext>();
        public RepoBase<UrunMagaza, ETicaretContext> UrunMagazaRepo { get; set; } = new Repo<UrunMagaza, ETicaretContext>();
        public RepoBase<Kategori, ETicaretContext> KategoriRepo { get; set; } = new Repo<Kategori, ETicaretContext>();
        public RepoBase<Magaza, ETicaretContext> MagazaRepo { get; set; } = new Repo<Magaza, ETicaretContext>();
        public UrunService()
        {
            _db = new ETicaretContext();
            Repo = new Repo<Urun, ETicaretContext>(_db);
            UrunMagazaRepo = new Repo<UrunMagaza, ETicaretContext>(_db);
            KategoriRepo = new Repo<Kategori, ETicaretContext>(_db);
            MagazaRepo = new Repo<Magaza, ETicaretContext>(_db);
        }

        public Result Add(UrunModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girdi�iniz �r�n ad�na sahip kay�t bulunmaktad�r!");
            Urun entity = new Urun()
            {
                Adi = model.Adi.Trim(),
                Aciklama = model.Aciklama?.Trim(),
                BirimFiyati = model.BirimFiyati.Value,
                KategoriId = model.KategoriId.Value,
                SonKullanmaTarihi = model.SonKullanmaTarihi,
                StokMiktari = model.StokMiktari.Value,
                UrunMagazalar = model.MagazaIdleri?.Select(magazaId => new UrunMagaza() { MagazaId = magazaId }).ToList(), //2 y�ntem

                Imaj = model.Imaj,
                ImajUzantisi = model.ImajUzantisi
            };
            //1.Y�ntem
            //entity.UrunMagazalar = new List<UrunMagaza>();
            //foreach(var magazaId in model.MagazaIdleri)
            //{
            //    entity.UrunMagazalar.Add(new UrunMagaza() { MagazaId = magazaId });
            //}

            Repo.Add(entity);
            model.Id = entity.Id; // wepApi de yeni �r�n id sine ula�abilmek i�in yazmal�s�n
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            UrunMagazaRepo.Delete(um => um.UrunId == id);
            Repo.Delete(u => u.Id == id);
            return new SuccessResult("�r�n ba�ar�yla silindi!");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<UrunModel> Query()
        {
            return Repo.Query("Kategori","UrunMagazalar").OrderBy(u => u.Adi).Select(u => new UrunModel()
            {
                Aciklama = u.Aciklama,
                Adi = u.Adi,
                BirimFiyati = u.BirimFiyati,
                Id = u.Id,
                KategoriId = u.KategoriId,
                SonKullanmaTarihi = u.SonKullanmaTarihi,
                StokMiktari = u.StokMiktari,

                BirimFiyatiDisplay = u.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                SonKullanmaTarihiDisplay = u.SonKullanmaTarihi.HasValue ? u.SonKullanmaTarihi.Value.ToString("yyyy.MM.dd") : "",
                KategoriAdiDisplay = u.Kategori.Adi,

                MagazalarDisplay = u.UrunMagazalar.Select(um => um.Magaza.Adi).ToList(),
                MagazaDisplay = string.Join("<br />" , u.UrunMagazalar.Select(um => um.Magaza.Adi)),

                Imaj = u.Imaj,
                ImajUzantisi = u.ImajUzantisi,
                ImgSrcDisplay = u.Imaj == null ? null : FileUtil.GetContentType(u.ImajUzantisi, true, true) + Convert.ToBase64String(u.Imaj)
            });
            
        }

        public Result Update(UrunModel model)
        {
            if (Repo.Query().Any(x => x.Adi.ToLower() == model.Adi.ToLower().Trim() && x.Id != model.Id))
                return new ErrorResult("Girmek istedi�iniz �r�n ad�na sahip kay�t bulunmaktad�r.");

            //1.Y�ntem
            //Urun entity = Repo.Query("UrunMagazalar").SingleOrDefault(u => u.Id == model.Id);            
            //if (entity.UrunMagazalar != null && entity.UrunMagazalar.Count > 0)
            //{
            //    foreach (var urunMagaza in entity.UrunMagazalar )
            //    {
            //        UrunMagazaRepo.Delete(urunMagaza, false); //false yazd�k ki her d�ng�de SaveChanges � �a��rmas�n, performans� d��mesin diye
            //    }
            //    UrunMagazaRepo.Save(); //Save i�lemini tek seferde burada yapt�k
            //}
            Urun entity = Repo.Query().SingleOrDefault(u => u.Id == model.Id);
            UrunMagazaRepo.Delete(um => um.UrunId == model.Id);

            entity.Adi = model.Adi.Trim();
            entity.Aciklama = model.Aciklama?.Trim();
            entity.BirimFiyati = model.BirimFiyati.Value;
            entity.KategoriId = model.KategoriId.Value;
            entity.SonKullanmaTarihi = model.SonKullanmaTarihi;
            entity.StokMiktari = model.StokMiktari.Value;

            entity.UrunMagazalar = model.MagazaIdleri?.Select(magazaId => new UrunMagaza() { MagazaId = magazaId }).ToList();

            if(model.Imaj != null)
            {
                entity.Imaj = model.Imaj;
                entity.ImajUzantisi = model.ImajUzantisi;
            }

            Repo.Update(entity);
            return new SuccessResult("�r�n ba�ar�yla kaydedildi!");
        }
        public void DeleteImage(int id)
        {
            var entity = Repo.Query().SingleOrDefault(u => u.Id == id);
            entity.Imaj = null;
            entity.ImajUzantisi = null;
            Repo.Update(entity);
        }
    }
}
