using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Router
{
    public class Degiskenler
    {
        
        public class KitapTuruEntity
        {
            public int ID { get; set; }
            public string Adi { get; set; }
        }
        public class YayinEviEntity
        {
            public int ID { get; set; }
            public string Adi { get; set; }
        }
        public class EntityKitap
        {
            public int ID { get; set; }
            public string Adi { get; set; }
            public string YazarAdi { get; set; }
            public int? YazarID { get; set; }
            public byte[] Resim { get; set; }
        }
        public class YazarEntity
        {
            public int ID { get; set; }
            public string AdiSoyadi { get; set; }
        }
        //public class EntityYazar
        //{
        //    public int ID { get; set; }
        //    public string Adi { get; set; }
        //    public string YazarAdi { get; set; }
        //    public int? YazarID { get; set; }
        //}

        public class EntityKullanici
        {
            public int ID { get; set; }
            public string KullaniciAdi { get; set; }
            public string Parola { get; set; }


        }
    }
}