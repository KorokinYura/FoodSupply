using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class SetOrder
    {
        [Key]
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdSet { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("IdUser")]
        public ApplicationUser User { get; set; }
        [ForeignKey("IdSet")]
        public GoodsSet GoodsSet { get; set; }
    }
}
