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
        [StringLength(200, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")] // veri taban�na varchar(200) yapt�k // bu atribute lar �zerine yazd���m�z de�i�kenler i�in ge�erlidir
        [DisplayName("Ad�")]
        public string Adi { get; set; }
        [StringLength(500, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")] // a��klama s�tunu i�in varchar(500) yapt�k
        [DisplayName("A��klamas�")]
        public string Aciklama { get; set; }
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Birim Fiyat�")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} pozitif bir de�er olmal�d�r!")]
        public double? BirimFiyati { get; set; } // de�er tipler i�in null alabilsin diye ? koyulur// de�er tipler i�in [Required] yaz�lmaz
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Stok Miktar�")]
        [Range(0,1000000, ErrorMessage = "{0} minimum {1} maksimum {2} olmal�d�r!")]
        public int? StokMiktari { get; set; }
        [DisplayName("Son Kullanma Tarihi")]
        public DateTime? SonKullanmaTarihi { get; set; } //null alabilsin diye ? koyduk
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        #endregion
        #region
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
        [Column(TypeName = "image")]
        [DisplayName("�maj")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        [DisplayName("�maj")]
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
