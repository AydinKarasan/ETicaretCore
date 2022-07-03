using AppCorev1.Business.Models.Ordering;
using AppCorev1.Business.Models.Paging;
using Business.Models.Filters;
using Business.Models.Report;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcWebUI.Areas.Raporlar.Models
{
    public class UrunRaporViewModel
    {
        public List<UrunRaporModel> Rapor { get; set; }
        public UrunRaporFiltreModel Filtre { get; set; }
        public SelectList Kategoriler { get; set; }
        public MultiSelectList Magazalar { get; set; }
        public PageModel Sayfa { get; set; }
        public SelectList Sayfalar { get; set; }
        public SelectList SayfadakiKayitSayilari { get; set; }
        public OrderModel Sira { get; set; }
        public SelectList Siralar { get; set; }


    }
}