using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class GoodsInSet
    {
        public int IdGoods { get; set; }
        public int IdSet { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("IdSet")]
        public GoodsSet GoodsSet { get; set; }
        [ForeignKey("IdGoods")]
        public Goods Goods { get; set; }
    }
}
