using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class GoodsSet
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
