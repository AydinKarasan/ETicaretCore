using AppCorev1.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    //[Table("EticaretUrunler")] // veri tabanýnda tablo oluþurken ismini burada belirleyebiliyoruz
    public class Urun : RecordBase
    {
        [Required] // Atributes
        [StringLength(200)] // veri tabanýna varchar(200) yaptýk // bu atribute lar üzerine yazdýðýmýz deðiþkenler için geçerlidir
        public string Adi { get; set; }
        [StringLength(500)] // açýklama sütunu için varchar(500) yaptýk
        public string Aciklama { get; set; }
        public double BirimFiyati { get; set; } // deðer tipler için null alabilsin diye ? koyulur// deðer tipler için [Required] yazýlmaz
        public int StokMiktari { get; set; }
        public DateTime? SonKullanmaTarihi { get; set; } //null alabilsin diye ? koyduk
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; } //bir ürünün bir kategorisi olabilir
        [Column(TypeName = "image")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        public List<UrunMagaza> UrunMagazalar{ get; set; } //bir ürünün birden çok maðazasý olabilir/bir maðazanýn birden çok ürünü olabilir
        
    }
}
