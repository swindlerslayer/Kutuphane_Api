using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebApplication1.Models.Data;
using static WebApplication1.Models.Router.Degiskenler;

namespace WebApplication1.Models.Router
{ 
public class DbKitap
    {
        public int Sayfadegeri { get; set; }

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
                    Resim = x.Resim,
                }).ToList();
                return kitaplar;
            }
        }
        public static bool EkleDuzenle(Kitap x)
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
                    return true;
                }
                else
                {
                    var kitap = db.Kitap.FirstOrDefault(y => y.ID == x.ID);

                    kitap.Adi = x.Adi;
                    kitap.SayfaSayisi = x.SayfaSayisi;
                    kitap.DegisiklikTarihi = DateTime.Now;
                    kitap.DegisiklikYapan = x.DegisiklikYapan;
                    kitap.Barkod = x.Barkod;
                    kitap.YazarID = x.YazarID;
                    kitap.KitapTurID = x.KitapTurID;
                    kitap.YayinEviID = x.YayinEviID;
                    kitap.Resim = x.Resim;


                    db.SaveChanges();
                    return false;
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
                if (kitapkontrol == null)
                {
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
        public static object KitapFiltreArama(int AranacakDeger)
        {
            SqlConnection OleCn = new SqlConnection(@"Data Source=BERKANT\SQL2016;Initial Catalog=Kutuphane;Persist Security Info=True;User ID=sa;Password=1;MultipleActiveResultSets=True;Application Name=EntityFramework");
            var Sayfadegeri = 15;

            OleCn.Open();
            var kalinanyer = AranacakDeger * Sayfadegeri;

            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = OleCn;
            Cmd.CommandText = ($"SELECT * FROM Kitap ORDER BY ID OFFSET {kalinanyer} ROWS FETCH NEXT 15 ROWS ONLY;");
            SqlDataAdapter Da = new SqlDataAdapter();

            Da.SelectCommand = Cmd;
            Cmd.ExecuteNonQuery();
            DataTable Ds = new DataTable();
            Da.Fill(Ds);
       


            return new { Data = Ds, PageCount = AranacakDeger +1 };
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
public class KitapPagination
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}