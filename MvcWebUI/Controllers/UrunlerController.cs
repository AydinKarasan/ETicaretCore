using AppCorev1.Utils;
using Business.Models;
using Business.Services;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebUI.Models;
using MvcWebUI.Settings;

namespace MvcWebUI.Controllers
{
    [Authorize] //burada tanımlayınca class içindeki bütün metodlar için geçerli olur
    public class UrunlerController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly IKategoriService _kategoriService;
        private readonly IMagazaService _magazaService;
        public UrunlerController(IUrunService urunService, IKategoriService kategoriService, IMagazaService magazaService)
        {
            _urunService = urunService;
            _kategoriService = kategoriService;
            _magazaService = magazaService;
        }

        // GET: Urunler
        //[Authorize(Roles = "Admin,Kullanıcı")]
        //[Authorize] //sistemde zaten bütün herkes görecek onun için tek tek yetki rolü yazmaya gerek yok
        [AllowAnonymous] //sisteme giriş yapmayan bile ürün listesini görebilsin diye yaptık Authorize ı devre dışı bıraktık
        public IActionResult Index()
        {
            List<UrunModel> urunler = _urunService.Query().ToList();
            UrunlerIndexViewModel viewModel = new UrunlerIndexViewModel()
            {
                Urunler = urunler
            };
            return View(viewModel);
        }

        // GET: Urunler/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Kayıt bulunamadı!"); //404 
            }
            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id.Value);

            if (urun == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(urun);
        }

        // GET: Urunler/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi");
            //var result = _magazaService.List();
            //ViewBag.Magazalar = new MultiSelectList(result.Data, "Id", "Adi"); // service deki list metodunu kullandık
            ViewBag.Magazalar = new MultiSelectList(_magazaService.Query().ToList(), "Id", "Adi");
            return View();

        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UrunModel urun, IFormFile imaj)
        {
            if (ModelState.IsValid)
            {
                if (ImajDosyasiniGuncelle(urun, imaj) == false)
                {
                    ModelState.AddModelError("", $"Dosya uzantıları : {AppSettings.ImajUzantilari} ve dosya boyutu maksimum : {AppSettings.ImajBoyutu} MB olmalıdır!");
                }
                else
                {
                    var result = _urunService.Add(urun);
                    if (result.IsSuccessful)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", result.Message);
                }
            }
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewBag.Magazalar = new MultiSelectList(_magazaService.Query().ToList(), "Id", "Adi");
            return View(urun);
        }
        private bool? ImajDosyasiniGuncelle(UrunModel model, IFormFile yuklenenImaj)
        {
            #region Dosya validasyonu
            bool? sonuc = null;
            
            if (yuklenenImaj != null && yuklenenImaj.Length > 0) // yüklenen imaj verisi varsa
            {
                sonuc = FileUtil.CheckFileExtension(yuklenenImaj.FileName, AppSettings.ImajUzantilari).IsSuccessful;
                                
                if (sonuc == true) // eğer imaj uzantısı validasyonunu geçtiyse imaj boyutunu valide edelim
                {
                    // 1 byte = 8 bits
                    // 1 kilobyte = 1024 bytes
                    // 1 megabyte = 1024 kilobytes = 1024 * 1024 bytes = 1.048.576 bytes
                    sonuc = FileUtil.CheckFileLenght(yuklenenImaj.Length, AppSettings.ImajBoyutu).IsSuccessful;
                }
            }
            #endregion

            #region Dosyanın kaydedilmesi
            if (sonuc == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    yuklenenImaj.CopyTo(memoryStream);
                    model.Imaj = memoryStream.ToArray();
                    model.ImajUzantisi = Path.GetExtension(yuklenenImaj.FileName);
                }
            }
            #endregion

            return sonuc;
        }

        // GET: Urunler/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == id);
            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewBag.Magazalar = new MultiSelectList(_magazaService.Query().ToList(), "Id", "Adi");
            return View(urun);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UrunModel urun, IFormFile imaj)
        {
            if (ModelState.IsValid)
            {
                if (ImajDosyasiniGuncelle(urun, imaj) == false)
                {
                    ModelState.AddModelError("", $"Dosya uzantıları : {AppSettings.ImajUzantilari} ve dosya boyutu maksimum : {AppSettings.ImajBoyutu} MB olmalıdır!");
                }
                else
                {
                    var result = _urunService.Update(urun);
                    if (result.IsSuccessful)
                    {
                        TempData["Mesaj"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", result.Message);
                }
            }
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewBag.Magazalar = new MultiSelectList(_magazaService.Query().ToList(), "Id", "Adi");
            return View(urun);
        }

        // GET: Urunler/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin"))) //Authorized kısmını yazmadan buraya yazarak yazabilirsin
                return RedirectToAction("Giris", "Hesaplar");

            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == id);

            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }

            return View(urun);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _urunService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int urunId)
        {
            _urunService.DeleteImage(urunId);
            return RedirectToAction(nameof(Details), new { id = urunId });
        }
        public IActionResult DownloadImage(int urunId)
        {
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == urunId);
            if (urun == null)
                return View("Hata", "Ürün bulunamadı!");
            string fileName = "Product" + urun.ImajUzantisi;
            //return File(urun.Imaj, FileUtil.GetContentType(urun.ImajUzantisi));
            return File(urun.Imaj, FileUtil.GetContentType(urun.ImajUzantisi),fileName);
        }

    }
}
