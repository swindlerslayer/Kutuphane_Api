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
    static class DbKitapTuru
    {

        public static object KitapTurGetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.KitapTuru.FirstOrDefault(x => x.ID == id);
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
        public static bool EkleDuzenle(KitapTuru y)
        {

            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                if (y.ID == 0)
                {
                    y.KayitTarihi = DateTime.Now;
                    db.KitapTuru.Add(y);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var kitapturu = db.KitapTuru.FirstOrDefault(x => x.ID == y.ID);

                    kitapturu.Adi = y.Adi;

                    db.SaveChanges();
                    return false;
                }
            }
        }

        public static object ListeyeEklee()
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var kitapturleri = db.KitapTuru.Select(x => new KitapTuruEntity
                {
                    ID = x.ID,
                    Adi = x.Adi
                }).ToList();
                return kitapturleri;
            }
        }

        public static bool sil(int y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var kitapturubulma = db.KitapTuru.FirstOrDefault(x => x.ID == y);
                var kitapturukontrol = db.Kitap.FirstOrDefault(x => x.KitapTurID == kitapturubulma.ID);
                if (kitapturukontrol != null)
                {
                    return false;
                }
                else
                {
                    if (kitapturubulma.ID == 0)
                    {
                        return false;
                    }
                    else
                    {


                        var silinecekyazar = db.KitapTuru.FirstOrDefault(x => x.Adi == kitapturubulma.Adi);
                        db.KitapTuru.Remove(silinecekyazar);
                        db.SaveChanges();
                        return true;


                    }
                }
            }
        }

        public static bool KTKontrol(KitapTuru kntrl)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var kitapturu = db.Kitap.FirstOrDefault(x => x.KitapTurID == kntrl.ID);
                if (kitapturu != null)
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
