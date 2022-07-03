using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MagazalarController : Controller
    {        
        private readonly IMagazaService _magazaService;

        public MagazalarController(IMagazaService magazaService)
        {
            _magazaService = magazaService;
        }

        // GET: MagazalarController
        public ActionResult Index()
        {
            //var model = _magazaService.Query().ToList();
            var result = _magazaService.List(); //buradaki List metodu servislerde listeleme için yazdığımız metod
            var model = result.Data;
            //var model = result.Data == null ? new List<MagazaModel>() : result.Data; // uygun yer servis // eger magaza yoksa hata almamak için yani Data null gelirse diye yapılır ama servislerde çözmelisin
            //var model = result.Data ?? new List<MagazaModel>(); //üsttekinin aynısı
            ViewBag.Mesaj = result.Message;
            return View(model);
        }

        // GET: MagazalarController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var magaza = _magazaService.Query().SingleOrDefault(m => m.Id == id);
            if (magaza == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(magaza);
        }

        // GET: MagazalarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MagazalarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MagazaModel magaza)
        {
            if (ModelState.IsValid)
            { 
                var result = _magazaService.Add(magaza);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(magaza);
        }

        // GET: MagazalarController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var magaza = _magazaService.Query().SingleOrDefault(m => m.Id == id);
            if (magaza == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(magaza);
        }

        // POST: MagazalarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MagazaModel magaza)
        {
            if (ModelState.IsValid)
            {
                var result = _magazaService.Update(magaza);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(magaza);
        }

        // GET: MagazalarController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var magaza = _magazaService.Query().SingleOrDefault(m => m.Id == id);

            if (magaza == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            return View(magaza);
        }

        // POST: MagazalarController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _magazaService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
