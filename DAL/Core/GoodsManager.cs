using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Core
{
    class GoodsManager
    {
        private readonly ApplicationDbContext _db;

        public GoodsManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateGoods(Goods goods)
        {
            _db.Goods.Add(goods);
            _db.SaveChanges();
        }
    }
}
