using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace c_mulakat_soru.Models
{
    public class KursTuru
    {
        [Key]// primary key olarak ayarlar
        public int Id { get; set; }  //kursun ıd si burada tutulur
        [Required(ErrorMessage ="kurs tür adı boş bırakılamaz!")]// not null, ad null olamaz anlamına gelmektedir
        [MaxLength(30)]// max karakter sayısı
        [DisplayName("Kurs türü adı")]// ekranda bu gözükecek!
        public String Ad { get; set; }// kursun adı 
    }
}
