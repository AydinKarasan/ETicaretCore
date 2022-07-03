using AppCorev1.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class MagazaModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")] // Atributes
        [StringLength(200, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")]
        [DisplayName("Adý")]
        public string Adi { get; set; }
        [DisplayName("Sanal")]
        public bool SanalMi { get; set; } = true;
        [Range(1, 5, ErrorMessage = "{0} {1} ile {2} arasýnda olmalýdýr!")]
        [DisplayName("Puaný")]
        public byte? Puani { get; set; }
        [DisplayName("Sanal")]
        public string SanalMiDisplay { get; set; }
    }
}
