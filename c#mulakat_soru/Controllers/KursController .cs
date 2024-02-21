using c_mulakat_soru.Models;
using c_mulakat_soru.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace c_mulakat_soru.Controllers
{
    public class KursController : Controller
    {
        private readonly IKursRepository _kursRepository; // önce bütün actionları controllerin içine gömdüm mvc yapısından faydalanmak 
        //için daha sonra veri tabanındaki tablolar arttığı için solid presipleri ve clean code ya uygun olması için yapımı
        // design pattern yapısına çevirerek clean code yazmış oldum.....
        public KursController(IKursRepository context)//uygulama köprüsü kuruyoruz
        {
            _kursRepository = context;// context aldığı parametredir
        }



        // verileri listeledik
        public IActionResult Index()
        {
            List<Kurs> objKursList = _kursRepository.GetAll().ToList();// veritabanınındaki listeleri çekmeye yarar
            return View(objKursList);// kurs listesini iletir
        }




         public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kurs kurs)
        {
            if (ModelState.IsValid) {// karakter girilirse yeni kayıt at girilmezse atma
                _kursRepository.Ekle(kurs); // girilen veriyi kurs turunun içinde sakla
                _kursRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                return RedirectToAction("Index", "Kurs"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
         return View();
    }





        public IActionResult Guncelle(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if(id==null || id == 0)  // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            {                        
                return NotFound();
            }
            Kurs? kursVt = _kursRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            return View(kursVt);
        }
        [HttpPost]
        public IActionResult Guncelle(Kurs kurs)
        {
            if (ModelState.IsValid)
            {// karakter girilirse yeni kayıt at girilmezse atma
                _kursRepository.Guncelle(kurs); // girilen veriyi kurs turunun içinde sakla
                _kursRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "kurs türü başarıyla güncellendi";// işlem tamamlanırsa ekranda gözükecek    
                return RedirectToAction("Index", "Kurs"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
            return View();
        }




         // get action
        public IActionResult Sil(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if (id == null || id == 0)  // eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            {
                return NotFound();
            }
            Kurs? kursVt = _kursRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir
            if (kursVt == null) { return NotFound(); }// eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            return View(kursVt);
        }
        [HttpPost,ActionName("Sil")]// iki isim de aynı oldugu için  burada actionname verdim hata mesajı almamak için
        public IActionResult SilPOST(int? id)
        {
            Kurs? kurs = _kursRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kurs == null){// eğer silinecek kurs turu boş veya gelmemiş ise geriye hata mesajı döndür
                return NotFound();
            }
            _kursRepository.Sil(kurs);// sil kaydet guncelle olsun bunlar repositoryden çağırdığım metodlarım
            _kursRepository.Kaydet();
            TempData["basarili"] = " kurs türü başarıyla silindi";// işlem tamamlanırsa ekranda bu gözükecek
            return RedirectToAction("Index", "Kurs");// silme işleminden sonra yönlendireceği sayfa
        }
    }
}
