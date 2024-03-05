using c_mulakat_soru.Models;
using c_mulakat_soru.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;// buradaa kullanacağımız başvuruları import ettik
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace c_mulakat_soru.Controllers
{
    public class KursController : Controller// controller sınıfından miras alıyorum
    {
        private readonly IKursRepository _kursRepository;// alt parametreleri burada türetiyorum
        private readonly IKursTuruRepository _kursTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        // önce bütün actionları controllerin içine gömdüm mvc yapısından faydalanmak 
        //için daha sonra veri tabanındaki tablolar arttığı için solid presipleri ve clean code ya uygun olması için yapımı
        // design pattern yapısına çevirerek clean code yazmış oldum.....
        public KursController(IKursRepository kursRepository, IKursTuruRepository kursTuruRepository , IWebHostEnvironment webHostEnvironment)//uygulama köprüsü kuruyoruz
        {
            _kursRepository = kursRepository;// context aldığı parametredir
            _kursTuruRepository = kursTuruRepository;// ctor işlemlerini burada tamamladım
            _webHostEnvironment = webHostEnvironment;
        }
//////////////////:// verileri listeledik//////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Index()
        {
           // List<Kurs> objKursList = _kursRepository.GetAll().ToList();// veritabanınındaki listeleri çekmeye yarar
            List<Kurs> objKursList = _kursRepository.GetAll(includeProps:"kursTuru").ToList();
            return View(objKursList);// kurs listesini iletir
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult EkleGuncelle(int? id)// ekleguncelle viewi int türünde id değeri alabilir ve null olabilir
        {
            IEnumerable<SelectListItem> KursTuruList = _kursTuruRepository.GetAll()//tümünü çek
            .Select(k => new SelectListItem
            { Text = k.Ad, Value = k.Id.ToString() }  );
            ViewBag.KursTuruList = KursTuruList;// çanta görünümü anlamındadır
            if(id==null|| id == 0)// id değeri boş veya 0 sa boş dön
            {
                return View();
            }
            else
            {   //guncelleme
                Kurs? kursVt = _kursRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
                if (kursVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
                return View(kursVt);// eğer id dolu gelirse kursvt yi veri tabanından bul çek döndür
            }
        }
        [HttpPost]// aynı metodlar karışmasın diye burada httppost özelliğini kullandım
        public IActionResult EkleGuncelle(Kurs kurs,IFormFile? file)// kurs parametresi alarak döndür  ama resim özelliği de var burada 
        {
            if (ModelState.IsValid) {// eğer boş gelirse ife gir
                var errors = ModelState.Values.SelectMany(x => x.Errors);// modeldeki hatalarımı burada buluyorum
                string wwwRootPath = _webHostEnvironment.WebRootPath;// burada resimlerden dolayı rootlara ekleme yapıyorum
                string KursPath = Path.Combine(wwwRootPath, @"img");// ortam değişkenlerine buradan manuel olarak ekleme yapıyorum
                if (file != null) // eğer resim dosyası boş ise
                {
                using (var fileStream=new FileStream(Path.Combine(KursPath, file.FileName),FileMode.Create))
                {
                    file.CopyTo(fileStream); // hata mesajı almamak için bunu yapıyorum
                }
                kurs.ResimUrl = @"\img\" + file.FileName;// resim için işlemlerimi burada yapıyorum

                }
                if (kurs.Id == 0)// eğer kursun id si 0 gelirse yani yeni kurs eklenmek istenirse
                {
                    _kursRepository.Ekle(kurs);// kurs repoma kurs parametrem altında ekleme yap
                    TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                }
                else
                {
                    _kursRepository.Guncelle(kurs);// eğer id dolu gelirse yani bir kurs vardır ekleme yapılmaz bu kursun üzerine güncelleme yapılır
                    TempData["basarili"] = "yeni kurs türü başarıyla güncellendi";// ekranda gözükecek işlem tamamlanırsa
                }
                // karakter girilirse yeni kayıt at girilmezse atma
                // girilen veriyi kurs turunun içinde sakla
                _kursRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez bunun altında savechanges metodu vardır
                //TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                return RedirectToAction("Index", "Kurs"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
         return View();// geriye dön
    }
/*/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Guncelle(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if(id==null || id == 0)  // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            {                        
                return NotFound();
            } 
            Kurs? kursVt = _kursRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kursVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
            return View(kursVt);}*/
        /*
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
        */
/////////////////////////////////////////////// get action/////////////////////////////////////////////////////////////////////////
        public IActionResult Sil(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {
            if (id == null || id == 0)  // eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            {
                return NotFound();// ekranda hata mesajı döndür
            }
            Kurs? kursVt = _kursRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir
            if (kursVt == null) { return NotFound(); }// eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            return View(kursVt);// vt deki kursu döndür
        }
        [HttpPost,ActionName("Sil")]// iki isim de aynı oldugu için  burada actionname verdim hata mesajı almamak için
        public IActionResult SilPOST(int? id)// id değeri alır ama belki boş olabilir
        {
            Kurs? kurs = _kursRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (kurs == null){// eğer silinecek kurs turu boş veya gelmemiş ise geriye hata mesajı döndür
                return NotFound();// ekranda hata mesajı gösterir
            }
            _kursRepository.Sil(kurs);// sil kaydet guncelle olsun bunlar repositoryden çağırdığım metodlarım
            _kursRepository.Kaydet();// kaydet metodu arkadan savechangesi çağırır o da kaydeder
            TempData["basarili"] = " kurs türü başarıyla silindi";// işlem tamamlanırsa ekranda bu gözükecek
            return RedirectToAction("Index", "Kurs");// silme işleminden sonra yönlendireceği sayfa
        }
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
