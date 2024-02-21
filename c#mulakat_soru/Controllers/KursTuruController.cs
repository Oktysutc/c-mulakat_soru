using c_mulakat_soru.Models;
using c_mulakat_soru.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace c_mulakat_soru.Controllers
{
    public class KursTuruController : Controller
    {
        private readonly IKursTuruRepository _kursTuruRepository; // önce bütün actionları controllerin içine gömdüm mvc yapısından faydalanmak 
        //için daha sonra veri tabanındaki tablolar arttığı için solid presipleri ve clean code ya uygun olması için yapımı
        // design pattern yapısına çevirerek clean code yazmış oldum.....
        public KursTuruController(IKursTuruRepository context)//uygulama köprüsü kuruyoruz
        {
            _kursTuruRepository = context;// context aldığı parametredir
        }



        // verileri listeledik
        public IActionResult Index()
        {
            List<KursTuru> objKursTuruList = _kursTuruRepository.GetAll().ToList();// veritabanınındaki listeleri çekmeye yarar
            return View(objKursTuruList);// kurs listesini iletir
        }




         public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KursTuru kursTuru)
        {
            if (ModelState.IsValid) {// karakter girilirse yeni kayıt at girilmezse atma
                _kursTuruRepository.Ekle(kursTuru); // girilen veriyi kurs turunun içinde sakla
                _kursTuruRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                return RedirectToAction("Index", "KursTuru"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
         return View();
    }





        public IActionResult Guncelle(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if(id==null || id == 0)  // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            {                        
                return NotFound();
            }
            KursTuru? kursTuruVt = _kursTuruRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursTuruVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            return View(kursTuruVt);
        }
        [HttpPost]
        public IActionResult Guncelle(KursTuru kursTuru)
        {
            if (ModelState.IsValid)
            {// karakter girilirse yeni kayıt at girilmezse atma
                _kursTuruRepository.Guncelle(kursTuru); // girilen veriyi kurs turunun içinde sakla
                _kursTuruRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "kurs türü başarıyla güncellendi";// işlem tamamlanırsa ekranda gözükecek    
                return RedirectToAction("Index", "KursTuru"); // yeni kurs kayıt olduktan sonra gideceği yol
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
            KursTuru? kursTuruVt = _kursTuruRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir
            if (kursTuruVt == null) { return NotFound(); }// eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            return View(kursTuruVt);
        }
        [HttpPost,ActionName("Sil")]// iki isim de aynı oldugu için  burada actionname verdim hata mesajı almamak için
        public IActionResult SilPOST(int? id)
        {
            KursTuru? kursTuru = _kursTuruRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursTuru == null){// eğer silinecek kurs turu boş veya gelmemiş ise geriye hata mesajı döndür
                return NotFound();
            }
            _kursTuruRepository.Sil(kursTuru);// sil kaydet guncelle olsun bunlar repositoryden çağırdığım metodlarım
            _kursTuruRepository.Kaydet();
            TempData["basarili"] = " kurs türü başarıyla silindi";// işlem tamamlanırsa ekranda bu gözükecek
            return RedirectToAction("Index", "KursTuru");// silme işleminden sonra yönlendireceği sayfa
        }
    }
}
