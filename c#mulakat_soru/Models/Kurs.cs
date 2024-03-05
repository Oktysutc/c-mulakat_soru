using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c_mulakat_soru.Models
{
    public class Kurs
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? KursAdi { get; set; }
        public string? Konu { get; set; }
        [Required]
        public string? Yayinlayan { get; set; }
        [Required]
        [Range(10,5000)]
        public double Fiyat { get; set; }
        [ValidateNever]
        public int KursTuruId { get; set; }
        [ForeignKey("KursTuruId")]//burada veritabınıdaki foreign key ilişkisi sağlandı
        [ValidateNever]
        public KursTuru kursTuru { get; set; }
        [ValidateNever]
        public string ResimUrl { get; set; }
        
    }
}
