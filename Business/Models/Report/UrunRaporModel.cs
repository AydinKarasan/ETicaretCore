using System.ComponentModel;

namespace Business.Models.Report
{
    public class UrunRaporModel
    {
        [DisplayName("�r�n Ad�")]
        public string urunAdi { get; set; }        
        public string Aciklama { get; set; }        
        public double? BirimFiyati { get; set; }
        [DisplayName("Stok Miktar�")]
        public int? StokMiktari { get; set; }
        
        public DateTime? SonKullanmaTarihi { get; set; } 
        
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyat�")]
        public string BirimFiyatiDisplay { get; set; }
        [DisplayName("Kategori")]
        public string KategoriAdiDisplay { get; set; }
        [DisplayName("Son Kullanma Tarihi (Y�l.Ay.G�n)")]
        public string SonKullanmaTarihiDisplay { get; set; }
        [DisplayName("Ma�azalar")]
        public List<int> MagazaIdleri { get; set; }
        [DisplayName("Ma�aza")]
        public string MagazaDisplay { get; set; }
        public int MagazaId { get; set; }
    }
}
