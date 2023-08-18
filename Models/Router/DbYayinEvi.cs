using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApplication1.Models.Router.Degiskenler;
using WebApplication1.Models.Data;
using System.Net;
using System.Net.Http;

namespace WebApplication1.Models.Router
{
    static class DbYayinEvi
    {
        public static object ListeyeEkle()
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var yayinevleri = db.YayinEvi.Select(x => new YayinEviEntity
                {
                    ID = x.ID,
                    Adi = x.Adi

                }).ToList();
                return yayinevleri;
            }
        }

        public static object YayineviGetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.YayinEvi.FirstOrDefault(x => x.ID == id);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool sil(int y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var yayinevibulma = db.YayinEvi.FirstOrDefault(x => x.ID == y);
                var yayinevikontrol = db.Kitap.FirstOrDefault(x => x.YayinEviID == yayinevibulma.ID);
                if (yayinevikontrol != null)
                {
                    return false;
                }
                else
                {
                    if (yayinevibulma.ID == 0)
                    {
                        return false;
                    }
                    else
                    {


                        var silinecekyayinevi = db.YayinEvi.FirstOrDefault(x => x.Adi == yayinevibulma.Adi);
                        db.YayinEvi.Remove(silinecekyayinevi);
                        db.SaveChanges();
                        return true;


                    }
                }
            }
        }

        public static bool EkleDuzenle(YayinEvi y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                if (y.ID == 0)
                {
                    var yayinevi = db.YayinEvi.FirstOrDefault(x => x.Adi == y.Adi);
                    // y.KayitTarihi = DateTime.Now;
                    if (yayinevi != null)
                    {
                        return false;
                    }
                    else
                    {
                        db.YayinEvi.Add(y);
                        db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    var yayinevi = db.YayinEvi.FirstOrDefault(x => x.ID == y.ID);
                    yayinevi.Adi = y.Adi;
                    db.SaveChanges();
                    return false;
                }
            }
        }

        public static bool YK(YayinEvi kntrl)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var yayinevi = db.YayinEvi.FirstOrDefault(x => x.Adi == kntrl.Adi);
                if (yayinevi != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
