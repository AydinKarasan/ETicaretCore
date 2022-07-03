using AppCorev1.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    //[Table("EticaretUrunler")] // veri taban�nda tablo olu�urken ismini burada belirleyebiliyoruz
    public class Urun : RecordBase
    {
        [Required] // Atributes
        [StringLength(200)] // veri taban�na varchar(200) yapt�k // bu atribute lar �zerine yazd���m�z de�i�kenler i�in ge�erlidir
        public string Adi { get; set; }
        [StringLength(500)] // a��klama s�tunu i�in varchar(500) yapt�k
        public string Aciklama { get; set; }
        public double BirimFiyati { get; set; } // de�er tipler i�in null alabilsin diye ? koyulur// de�er tipler i�in [Required] yaz�lmaz
        public int StokMiktari { get; set; }
        public DateTime? SonKullanmaTarihi { get; set; } //null alabilsin diye ? koyduk
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; } //bir �r�n�n bir kategorisi olabilir
        [Column(TypeName = "image")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        public List<UrunMagaza> UrunMagazalar{ get; set; } //bir �r�n�n birden �ok ma�azas� olabilir/bir ma�azan�n birden �ok �r�n� olabilir
        
    }
}
