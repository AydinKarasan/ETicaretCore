using System.ComponentModel;

namespace Business.Models.Filters
{
    public class UrunRaporFiltreModel
    {
        [DisplayName("Ürün Adý")]
        public string UrunAdi { get; set; }
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyatý")]
        public double? BirimFiyatiMininmum { get; set; }        
        public double? BirimFiyatiMaximum { get; set; }
        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihiMinimum { get; set; }
        public DateTime? SonKullanmaTarihiMaximum { get; set; }
        [DisplayName("Maðaza")]
        public List<int> MagazaIdleri { get; set; }
        public string UrunMagazaAdi { get; set; }
    }
}
