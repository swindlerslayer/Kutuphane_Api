using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models.Data;

using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace WebApiBearerTokenApp.OAuth
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
            context.Validated();
        }
        public string hash = "slkjgdngı0243582ej";
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

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string Kadi, Ksifre, SifreliKsifre;
            Ksifre = context.Password;
            Kadi = context.UserName;
            SifreliKsifre = Encrypt(Ksifre);

            // CORS ayarlarını set ediyoruz. -- Cross Domain yazım dan konu ile alakalı detaylı bilgi alabilirsiniz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //validation işlemlerini ve kontrollerini bu kısımda yapıyoruz , örnek olması için sabit değerler verildi ,
            //bu kısmı db den okuyacak şekilde bir yapı kurgulanabilir.


            // Kullanıcı adı ve şifre bilgilerini kontrol etmek için bir veritabanı sorgusu yapılması gerekmektedir.
            // Bu örnekte Entity Framework kullanarak bir veritabanı bağlantısı yapıldığını varsayalım.
            using (var dbContext = new KutuphaneEntities())
            {
                try
                {
                    var user = dbContext.Kullanici.FirstOrDefault(u => u.KullaniciAdi == Kadi && u.Parola == SifreliKsifre);
                    if (user != null)
                    {
                        // Eğer kullanıcı doğrulandıysa ClaimsIdentity (Kimlik oluşturuyoruz)
                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim("sub", context.UserName));// Identity özelliklerini ekliyoruz.
                        identity.AddClaim(new Claim("role", "user"));

                        context.Validated(identity);// Doğrulanmış olan kimliği context'e ekliyoruz.
                    }
                    else
                    {
                        // Eğer hata var ise bir hata mesajı gönderiyoruz. 
                        context.SetError("Oturum Hatası", "Kullanıcı adı ve şifre hatalıdır");
                    }
                }
                catch (Exception ex)
                {

                }

            }


        }
    }
}