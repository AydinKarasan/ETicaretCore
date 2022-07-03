using AppCorev1.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        #region
        //FluentValidation kullanýlabilir
        [Required(ErrorMessage = "{0} gereklidir!")] //[Required(ErrorMessage = "Adý gereklidir!")] place holder olmadan kullanýmý// display name neyse onu yazdýrmak için bu þekilde yapma
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")]
        [DisplayName("Adý")]
        public string Adi { get; set; }
        [DisplayName("Açýklamasý")]
        public string Aciklamasi { get; set; }
        #endregion
        #region
        [DisplayName("Ürün Sayýsý")]
        public int UrunSayisiDisplay { get; set; }
        #endregion
    }
}
