using AppCorev1.Business.Models;
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KategorilerController : Controller
    {
        
        private readonly IKategoriService _kategoriService;

        public KategorilerController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }
        public IActionResult Index() //action   
        {
            List<KategoriModel> model = _kategoriService.Query().ToList(); 
            //return View(model);
            return View("KategoriListesi", model);
        }
        [HttpGet] // bu action bir metoddur. 
        public IActionResult OlusturGetir() // ~/Kategoriler/OlusturGetir
        {

            return View("OlusturHtml");
        }
        [HttpPost] //veri alıp dataya kaydedeceğin form u post oluştur. veri tabanından geleni karşılayacak bir httppost oluştur
        public IActionResult OlusturGonder(string adi,string aciklamasi)
        {
            if (string.IsNullOrWhiteSpace(adi))
                return View("Hata!", "Kategori adı boş olamaz!");
            if (adi.Trim().Length > 100)
                return View("Hata!", "Kategori adı maksimum 100 karakter olmalıdır!");
            if(!string.IsNullOrWhiteSpace(aciklamasi)&& aciklamasi.Length > 4000)
                return View("Hata!", "Kategori açıklaması maksimum 4000 karakter olmalıdır!");

            KategoriModel model = new KategoriModel()
            {
                Aciklamasi = aciklamasi,
                Adi=adi
            };
            Result result = _kategoriService.Add(model);
            if (result.IsSuccessful)
            {
                //return RedirectToAction("Index");
                TempData["Mesaj"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = result.Message;
            return View("Hata!", result.Message);
        }
        public IActionResult Edit(int? id) //~/Kategoriler/Edit/1
        {
            //if(id==null)
            if(!id.HasValue)
            {
                return View("Hata", "Id gereklidir!");
            }
            KategoriModel model = _kategoriService.Query().SingleOrDefault(x => x.Id == id.Value);
            if(model == null)
            {
                return View("Hata!", "Kayıt bulunamadı!");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KategoriModel model)
        {
            if(ModelState.IsValid)
            {
                var result = _kategoriService.Update(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        public IActionResult Details(int? id) //~/Kategoriler/Details/1
        {
            if (!id.HasValue)
                return View("Hata!", "Id gereklidir!");
            KategoriModel model = _kategoriService.Query().SingleOrDefault(k => k.Id == id);
            if (model == null)
                return View("Hata!", "Kayıt bulunamadı!");
            return View(model);
        }
        public IActionResult Delete(int? id) //~/Kategoriler/Delete/4
        {
            if (id == null)
                return View("Hata!", "Id gereklidir!");
            var result =_kategoriService.Delete(id.Value);
            // silebilse de silemesede index e döneceği için issuccesful u kontrol etmedik
                TempData["Mesaj"] = result.Message;
                return RedirectToAction(nameof(Index));
            
        }

        /*
         IActionResult
         ActionResult
         ViewResult (View)  |  ContentResult (Content) | EmptyResult  |  FileContentResult (File) | HttpResult  | JavascriptResult  Javascript() | JsonResult  (Json()) | RedirectResults  
         */

       public RedirectResult RedirectExample()
        {
            return Redirect("https://bilgeadam.com");
        }

        public IActionResult GetHtml()
        {
            return Content("<b><i>Content result.</i></b>, text/GetHtml");
        }
        public IActionResult GetXml()
        {
            string xml= "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<Kategoriler>";
            List<KategoriModel> kategoriler = _kategoriService.Query().ToList();
            foreach(KategoriModel kategori in kategoriler)
            {
                xml += "<Kategoriler>";
                xml += "<Id>" + kategori.Id + "</Id>";
                xml += "<Adi>" + kategori.Adi + "</Adi>";
                xml += "<Aciklamasi>" + kategori.Aciklamasi + "</Aciklamasi>";
                xml += "</Kategoriler>";
            }
            xml += "</Kategoriler>";
            return Content(xml, "application/xml");
        }

        public string GetString()
        {
            return "String...";
        }
        public EmptyResult GetEmpty()
        {
            return new EmptyResult();
        }
    }
}
