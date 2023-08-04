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
    
    public class SellOrdersController : Controller
    {
        private readonly WarehouseManagementDbContext _context;

        public SellOrdersController(WarehouseManagementDbContext context)
        {
            _context = context;
        }


        // GET: SellOrders
        
        public async Task<IActionResult> Index()
        {
            var warehouseManagementDbContext = _context.SellOrders.Include(s => s.Supplier);
            return View(await warehouseManagementDbContext.ToListAsync());
        }

        // GET: SellOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SellOrders == null)
            {
                return NotFound();
            }

            var sellOrder = await _context.SellOrders
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellOrder == null)
            {
                return NotFound();
            }

            return View(sellOrder);
        }
        [Authorize]
        // GET: SellOrders/Create
        public IActionResult Create()
        {
            ViewData["Supplierid"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: SellOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,Supplierid,DiscountValue,DiscountPercentage,BeforDiscountTotal,AfterDiscount")] SellOrder sellOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Supplierid"] = new SelectList(_context.Suppliers, "Id", "Id", sellOrder.Supplierid);
            return View(sellOrder);
        }

        // GET: SellOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SellOrders == null)
            {
                return NotFound();
            }

            var sellOrder = await _context.SellOrders.FindAsync(id);
            if (sellOrder == null)
            {
                return NotFound();
            }
            ViewData["Supplierid"] = new SelectList(_context.Suppliers, "Id", "Id", sellOrder.Supplierid);
            return View(sellOrder);
        }

        // POST: SellOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,Supplierid,DiscountValue,DiscountPercentage,BeforDiscountTotal,AfterDiscount")] SellOrder sellOrder)
        {
            if (id != sellOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellOrderExists(sellOrder.Id))
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
            ViewData["Supplierid"] = new SelectList(_context.Suppliers, "Id", "Id", sellOrder.Supplierid);
            return View(sellOrder);
        }

        // GET: SellOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SellOrders == null)
            {
                return NotFound();
            }

            var sellOrder = await _context.SellOrders
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellOrder == null)
            {
                return NotFound();
            }

            return View(sellOrder);
        }

        // POST: SellOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SellOrders == null)
            {
                return Problem("Entity set 'WarehouseManagementDbContext.SellOrders'  is null.");
            }
            var sellOrder = await _context.SellOrders.FindAsync(id);
            if (sellOrder != null)
            {
                _context.SellOrders.Remove(sellOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellOrderExists(int id)
        {
          return _context.SellOrders.Any(e => e.Id == id);
        }
    }
}
