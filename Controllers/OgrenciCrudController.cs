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
    public class OgrenciCrudController : ApiController
    {


        [System.Web.Http.HttpGet]
        [Route("api/ogrencigetir")]
        public IHttpActionResult TekKitapGetirDetayli(int ID)
        {
            var kaydedildi = DbOgrenci.OgrenciGetir(ID);
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
        [Route("api/ogrenciekleduzenle")]
        public IHttpActionResult ekleduzenle([FromBody] Ogrenci request)
        {
            bool kaydedildi = DbOgrenci.EkleDuzenle(request);
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
        [Route("api/ogrencisil")]
        public IHttpActionResult OgrenciSil(int ID)
        {
            //Body'de ID ve AdiSoyadi şeklinde aldığımız json formatındaki değerleri DbKitapTurundaki sil metoduna iletiyor 
            bool kaydedildi = DbOgrenci.sil(ID);
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
        [Route("api/ogrencikontrol")]
        public IHttpActionResult OgrenciKontrol(Ogrenci id)
        {
            var ogrenci = DbOgrenci.OK(id);
            return Ok(ogrenci);
        }



        [System.Web.Http.HttpGet]
        [Route("api/ogrencilisteyeekle")]
        public IHttpActionResult OgrenciKitapListele()
        {
            var kaydedildi = DbOgrenci.ListeyeEkle();
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error"));
            }
        }
    }
}