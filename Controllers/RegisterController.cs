using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Data;
using WebApplication1.Models.Router;

namespace WebApplication1.Controllers
{
    public class RegisterController : ApiController
    {
        public string Encrypt(string sifre)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        string hash = "slkjgdngı0243582ej";

        [System.Web.Http.HttpPost]

        [Route("api/Register")]



        public IHttpActionResult Register([FromBody] Kullanici request)
        {
            string Kadi, Ksifre, SifreliKsifre;
            Ksifre = request.Parola;
            Kadi = request.KullaniciAdi;
            SifreliKsifre = Encrypt(Ksifre);

            //if kullanıcı adına bağlı bir ID var ise Kullanıcı adı kullanılıyor döndür 

            Kullanici kullanici = new Kullanici();
            kullanici.KullaniciAdi = Kadi;
            kullanici.Parola = SifreliKsifre;
            var res = DbKullanici.KullaniciGirisKontrol(Kadi, SifreliKsifre);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                bool kaydedildi = DbKullanici.Kayit(Kadi, SifreliKsifre);
                if (kaydedildi == true)
                {
                    return Ok(kaydedildi);
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Kayıt Başarısız"));
                }
            }

        }

    }
}