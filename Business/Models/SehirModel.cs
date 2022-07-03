using AppCorev1.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class SehirModel : RecordBase
    {
        [Required]
        [StringLength(250)]
        public string Adi { get; set; }
        public int UlkeId { get; set; }        
        
    }
}
