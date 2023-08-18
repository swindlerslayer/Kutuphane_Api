using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Data;
using WebApplication1.Models.Router;
using static WebApplication1.Models.Router.Degiskenler;


namespace WebApplication1.Controllers
{
    [Authorize]
    public class KitapCrudController : ApiController
    {
        KutuphaneEntities db = new KutuphaneEntities();

        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/resimsil")]
        public IHttpActionResult resimsil(int id)
        {
            Kitap selectedKitap = new Kitap();
            selectedKitap.ID = id;

            Kitap kaydedildi = DbKitap.resimsil(selectedKitap);
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Message describing the error here"));
            }
        }


        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/kitapsil")]
        //parametre olarak id'si verilen kitabı siler
        public IHttpActionResult KitapSil(int ID)
        {
            var kaydedildi = DbKitap.sil(ID);
            return Ok(kaydedildi);

        }



        [System.Web.Http.HttpPost]

        [System.Web.Http.Route("api/ekleduzenle")]
        public IHttpActionResult ekleduzenle([System.Web.Http.FromBody] Kitap request)
        {
            //ekleduzenle route'de json olarak aldığım verileri request olarak
            //DbKitap içerisindeki EkleDuzenle isimli metoda iletiyor
            var kaydedildi = DbKitap.EkleDuzenle(request);
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Kitap Başarıyla Güncellendi"));
            }
        }

        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/kitapgetir")]

        public IHttpActionResult TumKitapGetir(int ID)
        {
            var kaydedildi = DbKitap.KitapGetir(ID);
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

        [System.Web.Http.Route("api/kitapgetirfiltre")]


        public IHttpActionResult KitapFiltreGetir(int id)
        {


            var kaydedildi = DbKitap.KitapFiltreArama(id);
            if (kaydedildi != null)
            {
                return Ok(kaydedildi);
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error"));
            }
           // SELECT* FROM table_name LIMIT 10 OFFSET 40




        }


        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/kitaplisteyeekle")]

        public IHttpActionResult KitapListeyeEkle()
        {
            var kaydedildi = DbKitap.ListeyeEkle();
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

        [System.Web.Http.Route("api/kitaplistele")]

        public IHttpActionResult KitapListele(int ID)
        {
            var kaydedildi = DbKitap.ListeyeEkle();
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