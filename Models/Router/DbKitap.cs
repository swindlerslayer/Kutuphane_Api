using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.Data;
using static WebApplication1.Models.Router.Degiskenler;

namespace WebApplication1.Models.Router
{
    public class DbKitap
    {
        public static List<EntityKitap> ListeyeEkle()
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var kitaplar = db.Kitap.Select(x => new EntityKitap
                {
                    ID = x.ID,
                    Adi = x.Adi,
                    YazarID = x.YazarID,
                    YazarAdi = x.Yazar.AdiSoyadi,
                    Resim = x.Resim
                }).ToList();
                return kitaplar;
            }
        }
        public static Kitap EkleDuzenle(Kitap x)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                if (x.ID == 0)
                {

                    x.KayitTarihi = DateTime.Now;
                    db.Kitap.Add(new Kitap
                    {
                        ID = x.ID,
                        Adi = x.Adi,
                        SayfaSayisi = x.SayfaSayisi,
                        KitapTurID = x.KitapTurID,
                        YayinEviID = x.YayinEviID,
                        YazarID = x.YazarID,
                        Barkod = x.Barkod,
                        KayitYapan = x.KayitYapan,
                        KayitTarihi = x.KayitTarihi,
                        DegisiklikTarihi = x.DegisiklikTarihi,
                        DegisiklikYapan = x.DegisiklikYapan,
                        Resim = x.Resim
                    });
                    db.SaveChanges();
                    return x;
                }
                else
                {
                    var kitap = db.Kitap.FirstOrDefault(y => y.ID == x.ID);

                    kitap.Adi = x.Adi;
                    kitap.SayfaSayisi = x.SayfaSayisi;
                    kitap.DegisiklikTarihi = DateTime.Now;
                    kitap.DegisiklikYapan = x.DegisiklikYapan;
                    kitap.Barkod = x.Barkod;
                    kitap.YazarID = x.YazarID; // Güncellenen YazarID
                    kitap.KitapTurID = x.KitapTurID;
                    kitap.YayinEviID = x.YayinEviID; // Güncellenen YayinEviID
                    kitap.Resim = x.Resim;


                    db.SaveChanges();
                    return x;
                }
            }
        }

        public static bool KitapKontrolKontrol(Kitap kntrl)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {

                var kitap = db.Kitap.FirstOrDefault(x => x.Adi == kntrl.Adi);
                if (kitap != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Kitap resimsil(Kitap y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;


                if (y.ID == 0)
                {
                    return null;
                }
                else
                {
                    var kitap = db.Kitap.FirstOrDefault(x => x.ID == y.ID);
                    if (kitap != null)
                    {
                        kitap.Resim = null;
                        db.SaveChanges();
                        return kitap;
                    }
                    else
                    {
                        return null;
                    }
                }




            }

        }
        public static bool sil(int y)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                var kitapkontrol = db.KitapOgrenci.FirstOrDefault(x => x.KitapID == y);
                if(kitapkontrol == null) { 
                    db.Configuration.ProxyCreationEnabled = false;
                    var customer = db.Kitap.First(c => c.ID == y);
                    db.Kitap.Remove(customer);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }
        public static Kitap KitapGetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.Kitap.FirstOrDefault(x => x.ID == id);
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
