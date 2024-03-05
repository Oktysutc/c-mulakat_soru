using c_mulakat_soru.Models;
using c_mulakat_soru.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;// gerekli paketleri burada import ettim
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace c_mulakat_soru.Controllers
{
    public class KursTuruController : Controller// buradan miras aldım controller sınıfımdan
    {
        private readonly IKursTuruRepository _kursTuruRepository; // önce bütün actionları controllerin içine gömdüm mvc yapısından faydalanmak 
        //için daha sonra veri tabanındaki tablolar arttığı için solid presipleri ve clean code ya uygun olması için yapımı
        // design pattern yapısına çevirerek clean code yazmış oldum.....
        public KursTuruController(IKursTuruRepository context)//uygulama köprüsü kuruyoruz
        {
            _kursTuruRepository = context;// context aldığı parametredir  design patterna uygun
        }
//*verileri listeledik*//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Index() // index actionunun geriye döneceği şeyler burada
        {
            List<KursTuru> objKursTuruList = _kursTuruRepository.GetAll().ToList();// veritabanınındaki listeleri çekmeye yarar
            return View(objKursTuruList);// kurs listesini  çeker ve döndürür
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         public IActionResult Ekle()// ekle viewını arayıp bulur ve döndürür 
        {
            return View();
        }
        [HttpPost] // aynı metodlar hata vermesin diye httppost özelliğini kullandım
        public IActionResult Ekle(KursTuru kursTuru)  // parametre alarak ekle actionunu geriye döndür
        {
            if (ModelState.IsValid) {// karakter girilirse yeni kayıt at girilmezse atma
                _kursTuruRepository.Ekle(kursTuru); // girilen veriyi kurs turunun içinde sakla
                _kursTuruRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                return RedirectToAction("Index", "KursTuru"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
         return View(); // geriye döndür
    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Guncelle(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if(id==null || id == 0)  // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            {                        
                return NotFound();// ekranda hata mesajı gösterir
            }
            KursTuru? kursTuruVt = _kursTuruRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursTuruVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            return View(kursTuruVt); // kursturuvt listesini geriye döndür
        }
        [HttpPost]// aynı isimli metodlar hata vermesin diye httppost özelliğini kullandım
        public IActionResult Guncelle(KursTuru kursTuru)// guncelle actionunu parametre alarak geriye döndür
        {
            if (ModelState.IsValid)// eğer karakter girilirse yeni kayıt at girilmezse atma 
            {// karakter girilirse yeni kayıt at girilmezse atma
                _kursTuruRepository.Guncelle(kursTuru); // girilen veriyi kurs turunun içinde sakla
                _kursTuruRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                TempData["basarili"] = "kurs türü başarıyla güncellendi";// işlem tamamlanırsa ekranda gözükecek    
                return RedirectToAction("Index", "KursTuru"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
            return View();// geriye döndür
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// get action
        public IActionResult Sil(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if (id == null || id == 0)  // eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            {
                return NotFound();// ekranda hata mesajı döndür
            }
            KursTuru? kursTuruVt = _kursTuruRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir
            if (kursTuruVt == null) { return NotFound(); }// eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            return View(kursTuruVt); // kursturuvt metodunu geriye döndür
        }
        [HttpPost,ActionName("Sil")]// iki isim de aynı oldugu için  burada actionname verdim hata mesajı almamak için
        public IActionResult SilPOST(int? id) // silpost actionu  int türündeid değeri alabilir null olabilir
        {
            KursTuru? kursTuru = _kursTuruRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursTuru == null){// eğer silinecek kurs turu boş veya gelmemiş ise geriye hata mesajı döndür
                return NotFound();// ekranda hata mesajı döndür
            }
            _kursTuruRepository.Sil(kursTuru);// sil kaydet guncelle olsun bunlar repositoryden çağırdığım metodlarım
            _kursTuruRepository.Kaydet();//kaydet metodunun altında savechanges var ondan onu çek ve döndür
            TempData["basarili"] = " kurs türü başarıyla silindi";// işlem tamamlanırsa ekranda bu gözükecek
            return RedirectToAction("Index", "KursTuru");// silme işleminden sonra yönlendireceği sayfa
        }
    }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
