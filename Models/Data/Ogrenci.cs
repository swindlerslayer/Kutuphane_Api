//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ogrenci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogrenci()
        {
            this.KitapOgrenci = new HashSet<KitapOgrenci>();
        }
    
        public int ID { get; set; }
        public string AdiSoyadi { get; set; }
        public string KayitYapan { get; set; }
        public Nullable<System.DateTime> KayitTarihi { get; set; }
        public string DegisiklikYapan { get; set; }
        public Nullable<System.DateTime> DegisiklikTarihi { get; set; }
        public string Sinif { get; set; }
        public Nullable<int> OkulNo { get; set; }
        public string Bölüm { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KitapOgrenci> KitapOgrenci { get; set; }
    }
}