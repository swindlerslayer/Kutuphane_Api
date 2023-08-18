using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Data;
using WebApplication1.Models.Router;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class YazarCrudController : ApiController
    {
        KutuphaneEntities db = new KutuphaneEntities();

        [System.Web.Http.HttpGet]

        [Route("api/yazargetir")]

        public IHttpActionResult TekYazarGetir(int ID)
        {
            var kaydedildi = DbYazar.YazarGetir(ID);
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error"));
            }

        }
        [System.Web.Http.HttpGet]

        [Route("api/tumyazargetir")]

        public IHttpActionResult TumYazarGetir()
        {
            var kaydedildi = DbYazar.TumYazarGetir();
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error"));
            }

        }


        [System.Web.Http.HttpGet]
        [Route("api/yazarkontrol")]
        public IHttpActionResult YazarKontrol(Yazar id)
        {
            var yazarlar = DbYazar.YK(id);
            return Ok(yazarlar);
        }





        [System.Web.Http.HttpGet]
        //ListeyeEkle Metodundaki yazar bilgilerini döndürür
        [Route("api/yazarlisteyeekle")]

        public IHttpActionResult YazarGetir()
        {
            var yazarlar = DbYazar.ListeyeEklee();
            return Ok(yazarlar);
        }


        [System.Web.Http.HttpPost]

        [Route("api/yazarekleduzenle")]
        public IHttpActionResult ekleduzenle([FromBody] Yazar request)
        {
            //ekleduzenle route'de json olarak aldığım verileri request olarak
            //DbKitap içerisindeki EkleDuzenle isimli metoda iletiyor
            bool kaydedildi = DbYazar.EkleDuzenle(request);
            if (kaydedildi == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [System.Web.Http.HttpGet]

        [Route("api/yazarsil")]
        public IHttpActionResult YazarSil(int ID)
        {
            //Body'de ID ve AdiSoyadi şeklinde aldığımız json formatındaki değerleri Dbyazardaki sil metoduna iletiyor 
            bool kaydedildi = DbYazar.sil(ID);
            if (kaydedildi == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

    }
}