using AppCorev1.Records.Bases;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    public class UrunModel : RecordBase 
    {
        #region
        [Required(ErrorMessage = "{0} gereklidir!")] // Atributes
        [StringLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")] // veri tabanýna varchar(200) yaptýk // bu atribute lar üzerine yazdýðýmýz deðiþkenler için geçerlidir
        [DisplayName("Adý")]
        public string Adi { get; set; }
        [StringLength(500, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")] // açýklama sütunu için varchar(500) yaptýk
        [DisplayName("Açýklamasý")]
        public string Aciklama { get; set; }
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Birim Fiyatý")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} pozitif bir deðer olmalýdýr!")]
        public double? BirimFiyati { get; set; } // deðer tipler için null alabilsin diye ? koyulur// deðer tipler için [Required] yazýlmaz
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Stok Miktarý")]
        [Range(0,1000000, ErrorMessage = "{0} minimum {1} maksimum {2} olmalýdýr!")]
        public int? StokMiktari { get; set; }
        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihi { get; set; } //null alabilsin diye ? koyduk
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        #endregion
        #region
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
        [Column(TypeName = "image")]
        [DisplayName("Ýmaj")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        [DisplayName("Ýmaj")]
        public string ImgSrcDisplay { get; set; }

        public List<string> MagazalarDisplay { get; set; } //hepsiburada, MediaMarkt
        #endregion


    }
    public class UrunModelComparer : IEqualityComparer<UrunModel>
    {
        public bool Equals(UrunModel x, UrunModel y)
        {
            if (x.Id == y.Id)
                return true;
            return false;
        }

        public int GetHashCode([DisallowNull] UrunModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
