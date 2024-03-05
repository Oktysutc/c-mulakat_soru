using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// veri 
namespace c_mulakat_soru.Models
{
    public class Satinal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OgrenciId { get; set; }
        [ValidateNever]
        public int? KursId { get; set; }
        [ForeignKey("KursTuruId")]//burada veritabınıdaki foreign key ilişkisi sağlandı
        [ValidateNever]
        public Kurs Kurs { get; set;}

    }
}
