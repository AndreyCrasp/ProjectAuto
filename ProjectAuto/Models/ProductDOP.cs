namespace ProjectAuto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDOP")]
    public partial class ProductDOP
    {
        public int ID { get; set; }

        public int MainProduct { get; set; }

        public int AttachedProduct { get; set; }

        public virtual Product Product { get; set; }
    }
}
