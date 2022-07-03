using AppCorev1.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UlkeModel : RecordBase
    {
        [Required]
        [StringLength(200)]
        [DisplayName("Adý")]
        public string Adi { get; set; }
       
    }
}
