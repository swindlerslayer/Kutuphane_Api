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

    [System.Web.Http.Authorize]

    public class YayinEviCrudController : ApiController
    {
        KutuphaneEntities db = new KutuphaneEntities();


        [System.Web.Http.HttpGet]
        //ListeyeEkle Metodundaki yazar bilgilerini döndürür
        [Route("api/yayinevilisteyeekle")]

        public IHttpActionResult YayineviGetir()
        {
            var yazarlar = DbYayinEvi.ListeyeEkle();
            return Ok(yazarlar);
        }



        [System.Web.Http.HttpGet]

        [Route("api/yayinevigetir")]

        public IHttpActionResult TekYazarGetir(int ID)
        {
            var kaydedildi = DbYayinEvi.YayineviGetir(ID);
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error"));
            }

        }



        [System.Web.Http.HttpPost]

        [Route("api/yayineviekleduzenle")]
        public IHttpActionResult ekleduzenle([FromBody] YayinEvi request)
        {
            //ekleduzenle route'de json olarak aldığım verileri request olarak
            //DbKitap içerisindeki EkleDuzenle isimli metoda iletiyor
            bool kaydedildi = DbYayinEvi.EkleDuzenle(request);
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
        [Route("api/yayinevikontrol")]

        public IHttpActionResult YayineviKontrol(YayinEvi id)
        {
            var yayinevi = DbYayinEvi.YK(id);
            return Ok(yayinevi);
        }

        [System.Web.Http.HttpGet]

        [Route("api/yayinevisil")]
        public IHttpActionResult YayineviSil(int ID)
        {
            //Body'de ID ve AdiSoyadi şeklinde aldığımız json formatındaki değerleri Dbyazardaki sil metoduna iletiyor 
            bool kaydedildi = DbYayinEvi.sil(ID);
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