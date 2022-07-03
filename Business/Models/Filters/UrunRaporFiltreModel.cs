using System.ComponentModel;

namespace Business.Models.Filters
{
    public class UrunRaporFiltreModel
    {
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyat�")]
        public double? BirimFiyatiMininmum { get; set; }        
        public double? BirimFiyatiMaximum { get; set; }
        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihiMinimum { get; set; }
        public DateTime? SonKullanmaTarihiMaximum { get; set; }
        [DisplayName("Ma�aza")]
        public List<int> MagazaIdleri { get; set; }
        public string UrunMagazaAdi { get; set; }
    }
}
