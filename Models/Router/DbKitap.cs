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
        public static object KitapSayfaArama(int AranacakDeger)
        {
            SqlConnection OleCn = new SqlConnection(SQLKOMUTLAR.sqlconnectionstring());
            var Sayfadegeri = 15;

            OleCn.Open();
            var kalinanyer = AranacakDeger * Sayfadegeri;

            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = OleCn;
            Cmd.CommandText = SQLKOMUTLAR.paginationsql(kalinanyer);
            SqlDataAdapter Da = new SqlDataAdapter();

            Da.SelectCommand = Cmd;
            Cmd.ExecuteNonQuery();
            DataTable Ds = new DataTable();
            Da.Fill(Ds);
       


            return new { Data = Ds, PageCount = AranacakDeger +1 };
        }

        public static object KitapFiltreArama(string AranacakDeger)
        {
            SqlConnection OleCn = new SqlConnection(SQLKOMUTLAR.sqlconnectionstring());

            OleCn.Open();

            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = OleCn;
            Cmd.CommandText = SQLKOMUTLAR.filteringsql(AranacakDeger);
            SqlDataAdapter Da = new SqlDataAdapter();

            Da.SelectCommand = Cmd;
            Cmd.ExecuteNonQuery();
            DataTable Ds = new DataTable();
            Da.Fill(Ds);



            return Ds;
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
public class SQLKOMUTLAR
{

    internal static string sqlconnectionstring()
    {
        return @"Data Source=BERKANT\SQL2016;Initial Catalog=Kutuphane;Persist Security Info=True;User ID=sa;Password=1;MultipleActiveResultSets=True;Application Name=EntityFramework";
    }
    internal static string paginationsql(int aranacakDeger)
    {
        return $"SELECT K1.ID ,K1.Adi,K1.SayfaSayisi,K1.KitapTurID,K1.YayinEviID,K1.YazarID,K1.Barkod,K1.KayitYapan,K1.KayitTarihi,K1.DegisiklikYapan,K1.DegisiklikTarihi,K1.Resim,K2.AdiSoyadi, K3.Adi,k4.Adi FROM(SELECT * FROM Kitap ORDER BY ID OFFSET {aranacakDeger} ROWS FETCH NEXT 15 ROWS ONLY) AS K1 JOIN Yazar AS K2  ON K1.YazarID = K2.ID JOIN YayinEvi AS K3 ON K1.YayinEviID = K3.ID JOIN KitapTuru AS K4 ON k1.KitapTurID = k4.ID; ";
    }

    internal static string filteringsql(string aranacakDeger)
    {
        return $"SELECT k1.ID, k1.Adi,k1.SayfaSayisi,k1.KitapTurID,k1.YayinEviID,k1.YazarID,k1.Barkod,k1.KayitYapan,k1.KayitTarihi,k1.DegisiklikYapan,k1.DegisiklikTarihi,k1.Resim FROM Kitap AS K1 JOIN Yazar AS K2  ON K1.YazarID = K2.ID JOIN YayinEvi AS K3 ON K1.YayinEviID = K3.ID JOIN KitapTuru AS K4 ON k1.KitapTurID = k4.ID WHERE '{aranacakDeger}' IN (k1.Adi, K2.AdiSoyadi, k3.Adi); ";
    }


}