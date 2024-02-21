using System.ComponentModel.DataAnnotations;

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
        
    }
}
