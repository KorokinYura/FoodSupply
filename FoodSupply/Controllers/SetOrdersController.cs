using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using DAL;
using DAL.Models;

namespace FoodSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SetOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SetOrders
        [HttpGet]
        public IEnumerable<Set> GetSetOrders()
        {
            var sets = new List<Set>();

            var goodsInSets = _context.GoodsInSets.Include(gs => gs.Goods).Include(gs => gs.GoodsSet).ToList();

            foreach (var set in _context.GoodsSets)
            {
                var gInS = goodsInSets.Where(gs => gs.IdSet == set.Id);

                var setGoods = new List<SetGoods>();

                foreach (var g in gInS)
                {
                    setGoods.Add(new SetGoods()
                    {
                        Id = g.Goods.Id,
                        Name = g.Goods.Name,
                        Description = g.Goods.Description,
                        Price = g.Goods.Price,
                        Supplier = g.Goods.Supplier,
                        Type = g.Goods.Type,
                        Quantity = g.Quantity
                    });
                }

                sets.Add(new Set
                {
                    Id = set.Id,
                    Name = set.Name,
                    Goods = setGoods
                });
            }

            return sets;
        }

        // GET: api/SetOrders/5
        [HttpGet("{userId}")]
        public IEnumerable<OrderSet> GetSetOrder([FromRoute] string userId)
        {
            var orders = _context.SetOrders.Where(o => o.IdUser == userId).Include(s => s.GoodsSet);

            var retOrders = new List<OrderSet>();

            foreach (var o in orders)
            {
                retOrders.Add(new OrderSet()
                {
                    Id = o.Id,
                    IdUser = o.IdUser,
                    IdSet = o.IdSet,
                    SetName = o.GoodsSet.Name,
                    Date = o.Date
                });
            }

            return retOrders;
        }

        // PUT: api/SetOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetOrder([FromRoute] int id, [FromBody] SetOrder setOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(setOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetOrderExists(id))
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

        // POST: api/SetOrders
        [HttpPost("{id}")]
        public async Task<IActionResult> PostSetOrder([FromRoute] string id, [FromBody] SetOrder setOrder)
        {
            setOrder.IdUser = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SetOrders.Add(setOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetOrder", new { id = setOrder.Id }, setOrder);
        }

        // DELETE: api/SetOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var setOrder = await _context.SetOrders.FindAsync(id);
            if (setOrder == null)
            {
                return NotFound();
            }

            _context.SetOrders.Remove(setOrder);
            await _context.SaveChangesAsync();

            return Ok(setOrder);
        }

        private bool SetOrderExists(int id)
        {
            return _context.SetOrders.Any(e => e.Id == id);
        }
    }


    public class OrderSet
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdSet { get; set; }
        public string SetName { get; set; }
        public DateTime Date { get; set; }
    }
}