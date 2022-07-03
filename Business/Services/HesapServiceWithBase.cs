using AppCorev1.Business.Models;
using Business.Models;
using DataAccess.Enums;

namespace Business.Services
{
    public interface IHesapService 
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel model);
        Result Kayit(KullaniciKayitModel model);
    }
    public class HesapService : IHesapService // crud iþlemleri yapmadýðýmýz için IHesapService ni implement etmiyoruz
    {
        private readonly IKullaniciService _kullaniciService;
        public HesapService(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public Result<KullaniciModel> Giris(KullaniciGirisModel model)
        {
            var kullanici = _kullaniciService.Query().SingleOrDefault(k => k.KullaniciAdi == model.KullaniciAdi && k.Sifre == model.Sifre && k.AktifMi);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Girdiðiniz kullanýcý adý ve þifreye ait kayýt bulunamadý!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }              

        public Result Kayit(KullaniciKayitModel model)
        {
            KullaniciModel kullanici = new KullaniciModel()
            {
                AktifMi = true, // kullanici sisteme kayýt olduðunda aktif olsun diye true yaptýk
                RolId = (int)Roller.Kullanýcý,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                KullaniciDetayi = new KullaniciDetayiModel()
                {
                    Adres = model.KullaniciDetayi.Adres.Trim(),
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    UlkeId = model.KullaniciDetayi.UlkeId,
                    SehirId = model.KullaniciDetayi.SehirId,
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet
                }

            };
            return _kullaniciService.Add(kullanici);
        }
    }
}
