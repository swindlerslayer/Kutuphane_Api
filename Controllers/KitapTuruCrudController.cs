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

    public class KitapTuruCrudController : ApiController
    {
        KutuphaneEntities db = new KutuphaneEntities();


        [System.Web.Http.HttpGet]

        [Route("api/kitapturugetir")]

        public IHttpActionResult TekKitapTurGetir(int ID)
        {
            var kaydedildi = DbKitapTuru.KitapTurGetir(ID);
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

        //ListeyeEkle Metodundaki Kitap Türü bilgilerini döndürür
        [Route("api/kitapturulisteyeekle")]

        public IHttpActionResult KitapTuruGetir()
        {
            var yazarlar = DbKitapTuru.ListeyeEklee();
            return Ok(yazarlar);
        }


        [System.Web.Http.HttpGet]
        [Route("api/kitapturkontrol")]
        public IHttpActionResult KitapTurKontrol(KitapTuru id)
        {
            var yazarlar = DbKitapTuru.KTKontrol(id);
            return Ok(yazarlar);
        }



        [System.Web.Http.HttpPost]

        [Route("api/kitapturuekleduzenle")]
        public IHttpActionResult ekleduzenle([FromBody] KitapTuru request)
        {
            bool kaydedildi = DbKitapTuru.EkleDuzenle(request);
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
        [Route("api/kitapturusil")]
        public IHttpActionResult KitapTuruSil(int ID)
        {
            //Body'de ID ve AdiSoyadi şeklinde aldığımız json formatındaki değerleri DbKitapTurundaki sil metoduna iletiyor 
            bool kaydedildi = DbKitapTuru.sil(ID);
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