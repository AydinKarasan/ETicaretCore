using AppCorev1.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        #region
        //FluentValidation kullan�labilir
        [Required(ErrorMessage = "{0} gereklidir!")] //[Required(ErrorMessage = "Ad� gereklidir!")] place holder olmadan kullan�m�// display name neyse onu yazd�rmak i�in bu �ekilde yapma
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("Ad�")]
        public string Adi { get; set; }
        [DisplayName("A��klamas�")]
        public string Aciklamasi { get; set; }
        #endregion
        #region
        [DisplayName("�r�n Say�s�")]
        public int UrunSayisiDisplay { get; set; }
        #endregion
    }
}
