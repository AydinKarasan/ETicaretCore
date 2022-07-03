using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MvcWebUI.Controllers
{
    public class SepetController : Controller
    {
        private IUrunService _urunService;
        public SepetController(IUrunService urunService)
        {
            _urunService = urunService;
        }
        public IActionResult Ekle(int urunId)
        {
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == urunId);
            if (urun.StokMiktari == 0)
            {
                TempData["Mesaj"] = "Sepete eklenmek istenen ürün stokta bulunmamaktadır!";
                return RedirectToAction("Index", "Urunler");
            }
            string json;
            SepetElemanModel eleman;
            List<SepetElemanModel> sepet = new List<SepetElemanModel>();
            if (HttpContext.Session.GetString("sepetkey") != null)
            {
                json = HttpContext.Session.GetString("sepetkey");
                sepet = JsonConvert.DeserializeObject<List<SepetElemanModel>>(json);
            }

            eleman = new SepetElemanModel()
            {
                UrunId = urun.Id,
                UrunAdi = urun.Adi,
                BirimFiyati = urun.BirimFiyati ?? 0,
                KullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)
            };
            if (sepet.Count(s => s.UrunId == urunId) > urun.StokMiktari)
            {
                TempData["Mesaj"] = "Sepete eklenen ürün sayısı stoktan fazladır bulunmamaktadır!";
                return RedirectToAction("Index", "Urunler");
            }
            else
            {
                sepet.Add(eleman);
            }
            //nuget den newtonsoft u yükle
            json = JsonConvert.SerializeObject(sepet);
            HttpContext.Session.SetString("sepetkey", json);
            return RedirectToAction("Index", "Urunler");

        }

        public IActionResult Getir()
        {
            List<SepetElemanModel> sepet = new List<SepetElemanModel>();
            if (HttpContext.Session.GetString("sepetkey") != null)
            {
                string json = HttpContext.Session.GetString("sepetkey");
                sepet = JsonConvert.DeserializeObject<List<SepetElemanModel>>(json);
            }

            sepet = sepet.Where(s => s.KullaniciId == Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)).ToList();

            List<SepetElemanGroupByModel> sepetGroupBy = (from s in sepet
                                                         //group s by s.UrunAdi // tek bir özellik için gruplama yaparsan böyle yap
                                                         group s by new { s.UrunId, s.KullaniciId, s.UrunAdi}
                                                         into sGroupBy
                                                         select new SepetElemanGroupByModel()
                                                         {
                                                             UrunId = sGroupBy.Key.UrunId,
                                                             KullaniciId = sGroupBy.Key.KullaniciId,
                                                             UrunAdi =  sGroupBy.Key.UrunAdi,
                                                             ToplamUrunBirimFiyati = sGroupBy.Sum(sgb => sgb.BirimFiyati),
                                                             ToplamUrunBirimFiyatiDisplay = sGroupBy.Sum(sgb => sgb.BirimFiyati).ToString("C2"),
                                                             ToplamUrunAdedi = sGroupBy.Count()
                                                         }).ToList();
            sepetGroupBy = sepetGroupBy.OrderBy(sgb => sgb.UrunAdi).ToList();

            //return View(sepet);
            return View("GetirGroupBy", sepetGroupBy);
        }

        public IActionResult Temizle()
        {
            //HttpContext.Session.Clear();
            //HttpContext.Session.Remove("sepetkey");

            var sepet = new List<SepetElemanModel>();
            string sepetJson = HttpContext.Session.GetString("sepetkey");
            if(!string.IsNullOrWhiteSpace(sepetJson))
            {
                sepet = JsonConvert.DeserializeObject<List<SepetElemanModel>>(sepetJson);
            }
            var kullaniciSepeti = sepet.Where(s => s.KullaniciId == Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)).ToList();
            foreach(var eleman in kullaniciSepeti)
            {
                sepet.Remove(eleman);
            }
            sepetJson = JsonConvert.SerializeObject(sepet);
            HttpContext.Session.SetString("sepetkey", sepetJson);

            return RedirectToAction(nameof(Getir));

        }

        public IActionResult Sil(int urunId, int kullaniciId)
        {
            var sepet = new List<SepetElemanModel>();
            string sepetJson = HttpContext.Session.GetString("sepetkey");
            if (!string.IsNullOrWhiteSpace(sepetJson))
            {
                sepet = JsonConvert.DeserializeObject<List<SepetElemanModel>>(sepetJson);
            }
            var eleman = sepet.FirstOrDefault(s => s.UrunId == urunId && s.KullaniciId == kullaniciId);
            if(eleman != null)
            {
                sepet.Remove(eleman);
                sepetJson = JsonConvert.SerializeObject(sepet);
                HttpContext.Session.SetString("sepetkey", sepetJson);
            }
            return RedirectToAction(nameof(Getir));
        }
    }
}
