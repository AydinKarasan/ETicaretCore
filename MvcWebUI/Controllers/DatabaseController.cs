using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace MvcWebUI.Controllers
{
    public class DatabaseController : Controller //örnek veri girmek için oluşturduğumuz controller
    {        
        public IActionResult Seed()
        {
            //ETicaretContext db = new ETicaretContext();
            //// db işlemleri
            //db.Dispose();

            using(ETicaretContext db = new ETicaretContext())
            {
                var urunler = db.Urunler.ToList();
                db.Urunler.RemoveRange(urunler);

                var kategoriler = db.Kategoriler.ToList();
                //foreach(var kategori in kategoriler)
                //{
                //    db.Kategoriler.Remove(kategori);
                //}
                db.Kategoriler.RemoveRange(kategoriler);

                var kullaniciDetaylari = db.KullaniciDetaylari.ToList();
                db.KullaniciDetaylari.RemoveRange(kullaniciDetaylari);

                var kullanicilar = db.Kullanicilar.ToList();
                db.Kullanicilar.RemoveRange(kullanicilar);

                var sehirler = db.Sehirler.ToList();
                db.Sehirler.RemoveRange(sehirler);

                var ulkeler = db.Ulkeler.ToList();
                db.Ulkeler.RemoveRange(ulkeler);
                var roller = db.Roller.ToList();
                db.Roller.RemoveRange(roller);

                //id nin sıfırdan başlaması için burayı yazdık
                if (kategoriler.Count >0)

                db.Database.ExecuteSqlRaw("dbcc checkident('Urunler',reseed,0)");
                db.Database.ExecuteSqlRaw("dbcc checkident('Kategoriler',reseed,0)");
                //db işlemleri
                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Bilgisayar",
                    Urunler = new List<Urun>()
                    {
                        new Urun()
                        {
                            Adi = "Dizüstü Bilgisayar",
                            BirimFiyati = 3000.5,
                            StokMiktari = 10,
                            SonKullanmaTarihi = new DateTime(2032, 1, 27)
                        },
                        new Urun()
                        {
                            Adi = "Bilgisayar Faresi",
                            BirimFiyati = 20.5,
                            StokMiktari = 20,
                            SonKullanmaTarihi = DateTime.Parse("19.05.2027", new CultureInfo("tr-TR")) 
                            // İngilizce bölgesel ayar için: en-US, sadece tarih ve ondalık veri tipleri için CultureInfo kullanılmalı,
                            // ~/Program.cs içersinde tüm uygulama için tek seferde AppCore üzerinden tanımlanıp kullanılabilir.
                        },
                        new Urun()
                        {
                            Adi = "Bilgisayar Klavyesi",
                            BirimFiyati = 40,
                            StokMiktari = 21,
                            Aciklama = "Bilgisayar Bileşeni"
                        },
                         new Urun()
                        {
                            Adi = "Bilgisayar Monitörü",
                            BirimFiyati = 2500,
                            StokMiktari = 27,
                            Aciklama = "Bilgisayar Bileşeni"
                        }
                    }
                });
                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Ev Eğlence Sistemi",
                    Aciklamasi = "Ev Sinema Sistemleri ve Televizyonlar",
                    Urunler = new List<Urun>()
                    {
                        new Urun()
                        {
                            Adi = "Hoparlör",
                            BirimFiyati = 2500,
                            StokMiktari = 5
                        },
                        new Urun()
                        {
                            Adi = "Amfi",
                            BirimFiyati = 5000,
                            StokMiktari = 9
                        },
                        new Urun()
                        {
                            Adi = "Ekolayzer",
                            BirimFiyati = 1000,
                            StokMiktari = 11
                        }
                    }
                });
                db.SaveChanges();

                db.Ulkeler.Add(new Ulke()
                {
                    Adi = "Türkiye", //1
                    Sehirler = new List<Sehir>()
                    {
                        new Sehir()
                        {
                            Adi = "Ankara" //1
                        },
                        new Sehir()
                        {
                            Adi = "İstanbul" //2
                        }
                    }
                });

                db.SaveChanges();

                db.Roller.Add(new Rol()
                {
                    Adi = "Admin",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            RolId = 1,
                            AktifMi = true,
                            KullaniciAdi = "Cagil",
                            Sifre = "1234",
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Çankaya",
                                Eposta = "cagil.eticaret.com",
                                SehirId = 1,
                                UlkeId = 1
                            }
                        }
                    }
                });
                db.SaveChanges();
            }
            return Content("<label style=\"color:red;\"><b>İlk Veriller Oluşturuldu</b>","text/html",Encoding.UTF8);
        }
    }
}
