using System.ComponentModel;

namespace Business.Models.Report
{
    public class UrunRaporModel
    {
        [DisplayName("Ürün Adý")]
        public string urunAdi { get; set; }        
        public string Aciklama { get; set; }        
        public double? BirimFiyati { get; set; }
        [DisplayName("Stok Miktarý")]
        public int? StokMiktari { get; set; }
        
        public DateTime? SonKullanmaTarihi { get; set; } 
        
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyatý")]
        public string BirimFiyatiDisplay { get; set; }
        [DisplayName("Kategori")]
        public string KategoriAdiDisplay { get; set; }
        [DisplayName("Son Kullanma Tarihi (Yýl.Ay.Gün)")]
        public string SonKullanmaTarihiDisplay { get; set; }
        [DisplayName("Maðazalar")]
        public List<int> MagazaIdleri { get; set; }
        [DisplayName("Maðaza")]
        public string MagazaDisplay { get; set; }
        public int MagazaId { get; set; }
    }
}
