using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciDetayiModel
    {               
        [Required]
        [StringLength(200)]
        [EmailAddress(ErrorMessage ="{0} formatý uygun deðildir!")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }
        public Cinsiyetler Cinsiyet { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required(ErrorMessage ="Ülke seçiniz...")]
        [DisplayName("Ülke")]
        public int? UlkeId { get; set; }
        [Required(ErrorMessage ="Þehir seçiniz...")]
        [DisplayName("Þehir")]
        public int? SehirId { get; set; }
        
    }
}
