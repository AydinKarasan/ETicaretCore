using AppCorev1.Business.Models.Ordering;
using AppCorev1.Business.Models.Paging;
using Business.Models.Filters;
using Business.Services;
using Business.Services.Bases;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcWebUI.Areas.Raporlar.Models;

namespace MvcWebUI.Areas.Raporlar.Controllers
{
    [Area("Raporlar")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // Add service injections here
        private readonly IUrunRaporService _urunRaporService;
        private readonly IKategoriService _kategoriService;
        private readonly IMagazaService _magazaService;

        public HomeController(IUrunRaporService urunRaporService, IKategoriService kategoriService, IMagazaService magazaService)
        {
            _urunRaporService = urunRaporService;
            _kategoriService = kategoriService;
            _magazaService = magazaService;
        }

        // GET: Raporlar/Home
        public IActionResult Index(UrunRaporViewModel viewModel)
        {
            //if (viewModel.Filtre == null)
            //    viewModel.Filtre = new UrunRaporFiltreModel();
            viewModel.Filtre = viewModel.Filtre ?? new UrunRaporFiltreModel();
            viewModel.Sayfa = viewModel.Sayfa ?? new PageModel()
            {
                PageNumber = 1,
                RecordsPerPageCount = 5
            };

            viewModel.Sira = viewModel.Sira ?? new OrderModel()
            {
                IsDirectionAscending = true
            };

            var result = _urunRaporService.List(viewModel.Filtre, viewModel.Sayfa, viewModel.Sira);
            viewModel.Rapor = result.Data;

            viewModel.Kategoriler = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi");
            viewModel.Magazalar = new MultiSelectList(_magazaService.Query().ToList(), "Id", "Adi");

            //UrunRaporViewModel viewModel = new UrunRaporViewModel()
            //{
            //    Rapor = result.Data,
            //    Kategoriler = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi")
            //};
            viewModel.Sayfalar = new SelectList(viewModel.Sayfa.PageNumberList);
            viewModel.SayfadakiKayitSayilari = new SelectList(viewModel.Sayfa.RecordsPerPageCountList);

            viewModel.Siralar = new SelectList(_urunRaporService.GetSiralar());

            return View(viewModel);
        }
    }
}