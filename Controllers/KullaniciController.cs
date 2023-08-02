using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Router;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers
{

    public class KullaniciController : ApiController
    {
        string hash = "slkjgdngı0243582ej";
        public string Decrypt(string SifrelenmisDeger)
        {
            byte[] data = Convert.FromBase64String(SifrelenmisDeger);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
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
        [Route("api/kullanici/kullaniciKontrol")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult KullaniciKontrol(string kullaniciAdi, string parola)
        {         
                
            try
            {
               // var ss = Encrypt(password);
                var res = DbKullanici.KullaniciGirisKontrol(kullaniciAdi, parola);

                if (res = true)
                {
                    return Ok(res);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [Route("api/kullanici/kullaniciBul")]
        [HttpGet]
        public IHttpActionResult KullaniciBul(string kullaniciAdi, string parola)
        {

            try
            {
                // var ss = Encrypt(password);
            
                var res = DbKullanici.KullaniciBul(kullaniciAdi, parola);

                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        [Route("api/kullanici/kullaniciKayit")]
        [HttpPost]
        public IHttpActionResult KullaniciKayit([FromBody] Kullanici request)
        {

            try
            {
                // var ss = Encrypt(password);
                string kadi = request.KullaniciAdi;
                string ksifre = request.Parola;
                var res = DbKullanici.KullaniciGirisKontrol(kadi, ksifre);

                if (res == true)
                {
                    return Ok(res);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }
    }
}