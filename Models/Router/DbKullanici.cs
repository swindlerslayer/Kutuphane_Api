﻿using System;
using System.Linq;
using WebApplication1.Models.Data;
using System.Web.Http;
using ww = System.Web.Http;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Models.Router
{

    public static class hash
    {
        static string hashstring = "slkjgdngı0243582ej";
    }

    public static class DbKullanici
    {


        public static Kullanici KullaniciBul(string kullaniciadi, string kullaniciparola)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var kullanici = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == kullaniciadi && x.Parola == kullaniciparola);

                return kullanici;
            }
        }

        public static bool Kayit(string ka, string kp)
        {
            try
            {
                using (KutuphaneEntities db = new KutuphaneEntities())
                {
                    Kullanici klnc = new Kullanici();
                    klnc.KayitTarihi = DateTime.Now;
                    klnc.KayitYapan = ka;
                    klnc.Parola = kp;
                    var kullanici = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == ka);

                    if (kullanici != null)
                    {
                        return false;
                    }
                    else
                    {
                        db.Kullanici.Add(klnc);
                        db.SaveChanges();
                        return true;
                    }


                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public static bool KullaniciGirisKontrol(string kullaniciAdi, string parola)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var kullanici = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi && x.Parola == parola);
                if (kullanici == null)
                {
                    Kullanici k = new Kullanici();
                    k.KullaniciAdi = kullaniciAdi;
                    k.Parola = parola;
                    k.KayitTarihi = DateTime.Now;
                    db.Kullanici.Add(k);
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return true;
                }
            }
        }

    }


}
