using AppCorev1.Business.Models;
using AppCorev1.Business.Services.Bases;
using AppCorev1.DataAccess.Entityframework;
using AppCorev1.DataAccess.Entityframework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, ETicaretContext>
    {

    }
    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, ETicaretContext> Repo { get; set; } = new Repo<Kullanici, ETicaretContext>();

        public Result Add(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi == model.KullaniciAdi))
                return new ErrorResult("Girdiðiniz kullanýcý adýna sahip kayýt bulunmaktadýr!");
            if (Repo.Query().Any(k => k.KullaniciDetayi.Eposta == model.KullaniciDetayi.Eposta))
                return new ErrorResult("Girdiðiniz e-postaya sahip kayýt bulunmaktadýr!");

            Kullanici entity = new Kullanici()
            {
                AktifMi = model.AktifMi,
                RolId = model.RolId,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                KullaniciDetayi = new KullaniciDetayi()
                {
                    Adres = model.KullaniciDetayi.Adres,
                    Eposta = model.KullaniciDetayi.Eposta,
                    UlkeId = model.KullaniciDetayi.UlkeId.Value,
                    SehirId = model.KullaniciDetayi.SehirId.Value,
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet
                }
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KullaniciModel> Query() // kullanýcý verilerini sorgulayýp getirdik
        {
            return Repo.Query("Rol").Select(k => new KullaniciModel()
            {
                AktifMi = k.AktifMi,
                Id = k.Id,
                KullaniciAdi = k.KullaniciAdi,
                Sifre = k.Sifre,
                RolId =k.RolId,
                RolAdiDisplay = k.Rol.Adi
            });
        }

        public Result Update(KullaniciModel model)
        {
            throw new NotImplementedException();
        }
    }
}
