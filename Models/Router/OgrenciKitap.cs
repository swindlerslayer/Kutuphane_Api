using System.Collections.Generic;
using System.Linq;
using System;
using static WebApplication1.Models.Router.Degiskenler;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.Router
{
    public class OgrenciKitap
    {

        public static KitapOgrenci kitapogrencigetir(int id)
        {
            using (KutuphaneEntities db = new KutuphaneEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var data = db.KitapOgrenci.FirstOrDefault(x => x.ID == id);
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
                db.Configuration.ProxyCreationEnabled = false;

                var customer = db.KitapOgrenci.First(c => c.ID == y);

                db.KitapOgrenci.Remove(customer);

                db.SaveChanges();
                return true;

            }
        }

        public static bool EkleDuzenle(KitapOgrenci k)
        {
            try
            {
                using (KutuphaneEntities db = new KutuphaneEntities())
                {
                    db.Configuration.ProxyCreationEnabled = false;

                    if (k.ID == 0)
                    {
                        k.KayitTarihi = DateTime.Now;
                        db.KitapOgrenci.Add(k);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {

                        var kitapogrenci = db.KitapOgrenci.FirstOrDefault(x => x.ID == k.ID);

                        kitapogrenci.OgrenciID = k.OgrenciID;
                        kitapogrenci.KitapID = k.KitapID;
                        kitapogrenci.DegisiklikTarihi = DateTime.Now;
                        kitapogrenci.DegisiklikYapan = k.DegisiklikYapan;
                        kitapogrenci.KullanıcıID = k.KullanıcıID;

                        kitapogrenci.TeslimDurumu = k.TeslimDurumu;
                        kitapogrenci.TeslimTarihi = k.TeslimTarihi;

                        db.SaveChanges();
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<KitapViewModel> GetKitapListesi()
        {
            using (KutuphaneEntities dbContext = new KutuphaneEntities())
            {
                var kitapListesi = dbContext.Kitap.Select(k => new KitapViewModel
                {
                    ID = k.ID,
                    Adi = k.Adi,
                    YayinEviAdi = k.YayinEvi.Adi
                }).ToList();



                return kitapListesi;
            }
        }
    }
    public class OKgrid
    {

        public static List<OKViewModel> GetGridDoldur()
        {
            using (KutuphaneEntities dbContext = new KutuphaneEntities())
            {


                var kitapOgrenciListesi = dbContext.KitapOgrenci.Select(ko => new OKViewModel
                {
                    ID = ko.ID,
                    Adi = ko.Kitap.Adi,
                    AdiSoyadi = ko.Ogrenci.AdiSoyadi,
                    OkulNo = ko.Ogrenci.OkulNo,
                    YayinEviAdi = ko.Kitap.YayinEvi.Adi,
                    AlisTarihi = ko.AlisTarihi,
                    TeslimTarihi = ko.TeslimTarihi,
                    TeslimDurumu = ko.TeslimDurumu == true
                    ? true
                    : false

                }).ToList();

                return kitapOgrenciListesi;
            }
        }
    }
    public class KitapViewModel
    {
        public int ID { get; set; }
        public string Adi { get; set; }
        public string YayinEviAdi { get; set; }
    }


    public class OKViewModel
    {
        public int? ID { get; set; }

        public string AdiSoyadi { get; set; }
        public int? OkulNo { get; set; }
        public string Adi { get; set; }
        public string YayinEviAdi { get; set; }
        public DateTime? AlisTarihi { get; set; }
        public Nullable<DateTime> TeslimTarihi { get; set; }
        public bool TeslimDurumu { get; set; }

    }

}
