using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Data;
using WebApplication1.Models.Router;
using static WebApplication1.Models.Router.OKgrid;



namespace WebApplication1.Controllers
{
    [Authorize]

    public class KitapOgrenciController : ApiController
    {
        KutuphaneEntities db = new KutuphaneEntities();


        [System.Web.Http.HttpPost]
        [Route("api/kitapogrenciekleduzenle")]
        public IHttpActionResult ekleduzenle([FromBody] KitapOgrenci request)
        {
            bool kaydedildi = OgrenciKitap.EkleDuzenle(request);
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
        [Route("api/ogrencikitapteklisteyeekle")]
        public IHttpActionResult OgrenciKitapTekListele(int id)
        {
            var kaydedildi = OgrenciKitap.kitapogrencigetir(id);
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
        [Route("api/ogrkitaplisteyeekle")]
        public IHttpActionResult KitapListele()
        {
            var kaydedildi = OKgrid.GetGridDoldur();
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
        [Route("api/okitaplisteyeekle")]
        public IHttpActionResult OKitapListele()
        {
            var kaydedildi = OgrenciKitap.GetKitapListesi();
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
        [Route("api/ogrencikitaplisteyeekle")]
        public IHttpActionResult OgrenciKitaptekListele()
        {
            var kaydedildi = OgrenciKitap.GetKitapListesi();
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
        [Route("api/kitapogrencisil")]
        public IHttpActionResult KitapOgrenciSil(int ID)
        {
            //Body'de ID ve AdiSoyadi şeklinde aldığımız json formatındaki değerleri
            //controller'in class'ındaki sil metoduna iletiyor 
            bool kaydedildi = OgrenciKitap.sil(ID);
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