using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse_Management_2.Models;

namespace Warehouse_Management_2.Controllers
{
    [Authorize]
    public class SellOrderDetailsController : Controller
    {
        private readonly WarehouseManagementDbContext _context;

        public SellOrderDetailsController(WarehouseManagementDbContext context)
        {
            _context = context;
        }

        // GET: SellOrderDetails
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var warehouseManagementDbContext = _context.SellOrderDetails.Include(s => s.Order).Include(s => s.Product);
            return View(await warehouseManagementDbContext.ToListAsync());
        }

        // GET: SellOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SellOrderDetails == null)
            {
                return NotFound();
            }

            var sellOrderDetail = await _context.SellOrderDetails
                .Include(s => s.Order)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellOrderDetail == null)
            {
                return NotFound();
            }

            return View(sellOrderDetail);
        }
        [Authorize]
        // GET: SellOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["Orderid"] = new SelectList(_context.SellOrders, "Id", "Id");
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: SellOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productid,Orderid,Quentity,ItemTotal")] SellOrderDetail sellOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.SellOrders, "Id", "Id", sellOrderDetail.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", sellOrderDetail.Productid);
            return View(sellOrderDetail);
        }

        // GET: SellOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SellOrderDetails == null)
            {
                return NotFound();
            }

            var sellOrderDetail = await _context.SellOrderDetails.FindAsync(id);
            if (sellOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["Orderid"] = new SelectList(_context.SellOrders, "Id", "Id", sellOrderDetail.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", sellOrderDetail.Productid);
            return View(sellOrderDetail);
        }

        // POST: SellOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productid,Orderid,Quentity,ItemTotal")] SellOrderDetail sellOrderDetail)
        {
            if (id != sellOrderDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellOrderDetailExists(sellOrderDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.SellOrders, "Id", "Id", sellOrderDetail.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", sellOrderDetail.Productid);
            return View(sellOrderDetail);
        }

        // GET: SellOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SellOrderDetails == null)
            {
                return NotFound();
            }

            var sellOrderDetail = await _context.SellOrderDetails
                .Include(s => s.Order)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellOrderDetail == null)
            {
                return NotFound();
            }

            return View(sellOrderDetail);
        }

        // POST: SellOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SellOrderDetails == null)
            {
                return Problem("Entity set 'WarehouseManagementDbContext.SellOrderDetails'  is null.");
            }
            var sellOrderDetail = await _context.SellOrderDetails.FindAsync(id);
            if (sellOrderDetail != null)
            {
                _context.SellOrderDetails.Remove(sellOrderDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellOrderDetailExists(int id)
        {
          return _context.SellOrderDetails.Any(e => e.Id == id);
        }
    }
}
