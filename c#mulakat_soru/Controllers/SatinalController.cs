using c_mulakat_soru.Models;
using c_mulakat_soru.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;// burada kullanılan başvuruları import ettim
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace c_mulakat_soru.Controllers
{
    public class SatinalController : Controller// controller sınıfınfdan miras aldım
    {
        private readonly ISatinalRepository _SatinalRepository;// önce mvc yapısına uygun bir yapı vardı burada ama proje büyükdükçe kod tekrarı oldu ve yapıyı design pattern yapısı
        private readonly IKursRepository _kursRepository;
        // private readonly IKursTuruRepository _kursTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        // önce bütün actionları controllerin içine gömdüm mvc yapısından faydalanmak 
        //için daha sonra veri tabanındaki tablolar arttığı için solid presipleri ve clean code ya uygun olması için yapımı
        // design pattern yapısına çevirerek clean code yazmış oldum.....
        public SatinalController(ISatinalRepository SatinalRepository, IKursRepository kursRepository , IWebHostEnvironment webHostEnvironment)//uygulama köprüsü kuruyoruz
        {
            _SatinalRepository = SatinalRepository;// context aldığı parametredir
            _kursRepository = kursRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        


        // verileri listeledik
        public IActionResult Index()
        {
           // List<Kurs> objKursList = _kursRepository.GetAll().ToList();// veritabanınındaki listeleri çekmeye yarar
            List<Satinal> objSatinalList = _SatinalRepository.GetAll(includeProps: "Kurs").ToList();
            return View(objSatinalList);// kurs listesini iletir
        }




         public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KursList = _kursRepository.GetAll()
               .Select(k => new SelectListItem
               { Text = k.KursAdi, Value = k.Id.ToString() });
            ViewBag.KursList = KursList;
            if(id==null|| id == 0)
            {
                return View();
            }
            else
            {
                //guncelleme
                Satinal? SatinalVt = _SatinalRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
                if (SatinalVt == null) { return NotFound(); }// // eğer güncellenecek id seçilmemiş veya gelmememiş ise// geriye hata mesajı döndür
                return View(SatinalVt);
            }
        }
        [HttpPost]
        public IActionResult EkleGuncelle(Satinal satinal)
        {
            
            if (ModelState.IsValid) {
                if (satinal.Id == 0)
                {
                    _SatinalRepository.Ekle(satinal);
                    TempData["basarili"] = "yeni satın alma işlemi başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                }
                else
                {
                    _SatinalRepository.Guncelle(satinal);
                    TempData["basarili"] = " satın alma işlemi başarıyla güncellendi";// ekranda gözükecek işlem tamamlanırsa

                }
                // karakter girilirse yeni kayıt at girilmezse atma
                // girilen veriyi kurs turunun içinde sakla
                _SatinalRepository.Kaydet();// bunu yapmazsanız bilgiler veri tabanına eklenmez
                //TempData["basarili"] = "yeni kurs türü başarıyla oluşturuldu";// ekranda gözükecek işlem tamamlanırsa
                return RedirectToAction("Index", "Satinal"); // yeni kurs kayıt olduktan sonra gideceği yol
            }
         return View();
    }




        /*
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


         // get action
        public IActionResult Sil(int? id)// güncellenecek id nin parametresi null olabilir uygulama çökmemesi için
        {

            IEnumerable<SelectListItem> KursList = _kursRepository.GetAll()
              .Select(k => new SelectListItem
              { Text = k.KursAdi, Value = k.Id.ToString() });
            ViewBag.KursList = KursList;
            if (id == null || id == 0)  // eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            {
                return NotFound();
            }
            Satinal? SatinalVt = _SatinalRepository.Get(u=>u.Id==id);// veri tabanından o id yi bul bana getir
            if (SatinalVt == null) { return NotFound(); }// eğer silinecek id gelmemiş veya seçilmemiş ise geriye hata mesajı döndür
            return View(SatinalVt);
        }
        [HttpPost,ActionName("Sil")]// iki isim de aynı oldugu için  burada actionname verdim hata mesajı almamak için
        public IActionResult SilPOST(int? id)
        {
            Satinal? Satinal = _SatinalRepository.Get(u => u.Id == id);// veri tabanından o id yi bul bana getir   Expression<Func<T, bool>> filtre   aynı zamanda bu filtreleme anlamına da gelir
            if (Satinal == null){// eğer silinecek kurs turu boş veya gelmemiş ise geriye hata mesajı döndür
                return NotFound();
            }
            _SatinalRepository.Sil(Satinal);// sil kaydet guncelle olsun bunlar repositoryden çağırdığım metodlarım
            _SatinalRepository.Kaydet();
            TempData["basarili"] = " kurs türü başarıyla silindi";// işlem tamamlanırsa ekranda bu gözükecek
            return RedirectToAction("Index", "Satinal");// silme işleminden sonra yönlendireceği sayfa
        }
    }
}
