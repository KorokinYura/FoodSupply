using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace FoodSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsSetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GoodsSetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GoodsSets
        [HttpGet]
        public IEnumerable<GoodsSet> GetGoodsSets()
        {
            return _context.GoodsSets;
        }

        // GET: api/GoodsSets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodsSet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goodsSet = await _context.GoodsSets.FindAsync(id);

            if (goodsSet == null)
            {
                return NotFound();
            }

            return Ok(goodsSet);
        }

        // PUT: api/GoodsSets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodsSet([FromRoute] int id, [FromBody] GoodsSet goodsSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodsSet.Id)
            {
                return BadRequest();
            }

            _context.Entry(goodsSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsSetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GoodsSets
        [HttpPost]
        public async Task<IActionResult> PostGoodsSet([FromBody] Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var goodsSet = new GoodsSet();
            goodsSet.Name = set.Name;

            _context.GoodsSets.Add(goodsSet);
            _context.SaveChanges();


            foreach(var g in set.Goods)
            {
                _context.GoodsInSets.Add(new GoodsInSet() { IdGoods = g.Id, IdSet = goodsSet.Id, Quantity = g.Quantity });
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodsSet", new { id = goodsSet.Id }, goodsSet);
        }

        // DELETE: api/GoodsSets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodsSet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goodsSet = await _context.GoodsSets.FindAsync(id);
            if (goodsSet == null)
            {
                return NotFound();
            }

            _context.GoodsSets.Remove(goodsSet);
            await _context.SaveChangesAsync();

            return Ok(goodsSet);
        }

        private bool GoodsSetExists(int id)
        {
            return _context.GoodsSets.Any(e => e.Id == id);
        }
    }



    public class Set
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<SetGoods> Goods { get; set; }
    }

    public class SetGoods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public string Type { get; set; }
        public string Supplier { get; set; }
        
        public int Quantity { get; set; }
    }
}