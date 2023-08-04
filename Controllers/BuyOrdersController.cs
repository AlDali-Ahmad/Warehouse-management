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
    public class BuyOrdersController : Controller
    {
        private readonly WarehouseManagementDbContext _context;

        public BuyOrdersController(WarehouseManagementDbContext context)
        {
            _context = context;
        }

        // GET: BuyOrders
        public async Task<IActionResult> Index()
        {
            var warehouseManagementDbContext = _context.BuyOrders.Include(b => b.Customer).Include(b => b.Employee);
            return View(await warehouseManagementDbContext.ToListAsync());
        }

        // GET: BuyOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BuyOrders == null)
            {
                return NotFound();
            }

            var buyOrder = await _context.BuyOrders
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyOrder == null)
            {
                return NotFound();
            }

            return View(buyOrder);
        }
        [Authorize]
        // GET: BuyOrders/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: BuyOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,BeforDiscountTotal,AfterDiscount,DiscountPercent,DiscountValue,Customerid,Employeeid")] BuyOrder buyOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", buyOrder.Customerid);
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Id", "Id", buyOrder.Employeeid);
            return View(buyOrder);
        }

        // GET: BuyOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BuyOrders == null)
            {
                return NotFound();
            }

            var buyOrder = await _context.BuyOrders.FindAsync(id);
            if (buyOrder == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", buyOrder.Customerid);
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Id", "Id", buyOrder.Employeeid);
            return View(buyOrder);
        }

        // POST: BuyOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,BeforDiscountTotal,AfterDiscount,DiscountPercent,DiscountValue,Customerid,Employeeid")] BuyOrder buyOrder)
        {
            if (id != buyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyOrderExists(buyOrder.Id))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", buyOrder.Customerid);
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Id", "Id", buyOrder.Employeeid);
            return View(buyOrder);
        }

        // GET: BuyOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BuyOrders == null)
            {
                return NotFound();
            }

            var buyOrder = await _context.BuyOrders
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyOrder == null)
            {
                return NotFound();
            }

            return View(buyOrder);
        }

        // POST: BuyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BuyOrders == null)
            {
                return Problem("Entity set 'WarehouseManagementDbContext.BuyOrders'  is null.");
            }
            var buyOrder = await _context.BuyOrders.FindAsync(id);
            if (buyOrder != null)
            {
                _context.BuyOrders.Remove(buyOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyOrderExists(int id)
        {
          return _context.BuyOrders.Any(e => e.Id == id);
        }
    }
}
