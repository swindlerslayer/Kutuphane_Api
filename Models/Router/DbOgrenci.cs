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
    public class DbOgrenci
    {
        public static bool EkleDuzenle(Ogrenci y)
        {

            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                if (y.ID == 0)
                {
                    y.KayitTarihi = DateTime.Now;
                    db.Ogrenci.Add(y);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var ogrenci = db.Ogrenci.FirstOrDefault(x => x.ID == y.ID);

                    ogrenci.AdiSoyadi = y.AdiSoyadi;
                    ogrenci.Sinif = y.Sinif;
                    ogrenci.Bölüm = y.Bölüm;
                    ogrenci.OkulNo = y.OkulNo;
                    

                    db.SaveChanges();
                    return false;
                }
            }
        }


        public static Ogrenci OgrenciGetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.Ogrenci.FirstOrDefault(x => x.ID == id);
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
        public static object ListeyeEkle()
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var ogrenciler = db.Ogrenci.Select(x => new OgrenciViewModel
                {
                    ID = x.ID,
                    AdiSoyadi = x.AdiSoyadi,
                    OkulNo = x.OkulNo
                    
                }).ToList();
                return ogrenciler;
            }
        }
        public static bool sil(int y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var ogrencibulma = db.Ogrenci.FirstOrDefault(x => x.ID == y);
                var ogrencikontrol = db.KitapOgrenci.FirstOrDefault(x => x.OgrenciID == ogrencibulma.ID);
                if (ogrencikontrol != null)
                {
                    return false;
                }
                else
                {
                    if (ogrencibulma.ID == 0)
                    {
                        return false;
                    }
                    else
                    {


                        var silinecekogrenci = db.Ogrenci.FirstOrDefault(x => x.AdiSoyadi == ogrencibulma.AdiSoyadi);
                        db.Ogrenci.Remove(silinecekogrenci);
                        db.SaveChanges();
                        return true;


                    }
                }
            }
        }

        public static bool OK(Ogrenci id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {


                var yazar = db.KitapOgrenci.FirstOrDefault(x => x.OgrenciID == id.ID);
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
      
    }

    public class OgrenciViewModel
    {
        public int ID { get; set; }
        public string AdiSoyadi { get; set; }
        public int? OkulNo { get; set; }
    }
}
