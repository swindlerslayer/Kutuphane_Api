using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApplication1.Models.Router.Degiskenler;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.Router
{
    public class DbYazar
    {

        public static bool EkleDuzenle(Yazar y)
        {

            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                if (y.ID == 0)
                {
                    y.KayitTarihi = DateTime.Now;
                    db.Yazar.Add(y);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var yazar = db.Yazar.FirstOrDefault(x => x.ID == y.ID);

                    yazar.AdiSoyadi = y.AdiSoyadi;

                    db.SaveChanges();
                    return false;
                }
            }
        }

        public static object ListeyeEklee()
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var yazarlar = db.Yazar.Select(x => new YazarEntity
                {
                    ID = x.ID,
                    AdiSoyadi = x.AdiSoyadi

                }).ToList();
                return yazarlar;
            }
        }     

        public static bool sil(int y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var yazarbulma = db.Yazar.FirstOrDefault(x => x.ID == y);
                var yazarkontrol = db.Kitap.FirstOrDefault(x => x.YazarID == yazarbulma.ID);
                if (yazarkontrol != null)
                {
                    return false;
                }
                else
                {
                    if (yazarbulma.ID == 0)
                    {
                        return false;
                    }
                    else
                    {
                        
                       
                        var silinecekyazar = db.Yazar.FirstOrDefault(x => x.AdiSoyadi == yazarbulma.AdiSoyadi);
                        db.Yazar.Remove(silinecekyazar);
                        db.SaveChanges();
                        return true;
                        
                    
                    }
                }
            }
        }

        public static bool YK(Yazar id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
      

                var yazar = db.Kitap.FirstOrDefault(x => x.YazarID == id.ID);
                if (yazar != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static object YazarGetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.Yazar.FirstOrDefault(x => x.ID == id);
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

    }
}
