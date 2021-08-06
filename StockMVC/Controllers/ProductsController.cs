using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockMVC.Data;
using StockMVC.Models;
using Microsoft.AspNetCore.Authorization;
using StockMVC.ViewModel;

namespace StockMVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category).OrderBy(m => m.ProductName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.ProductCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Price,CategoryId")] Product product, StockLevel stockLevel, WholesaleStockLevel wholesaleStockLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                stockLevel.ProductId = product.ProductId;
                _context.Add(stockLevel);
                wholesaleStockLevel.ProductId = product.ProductId;
                _context.Add(wholesaleStockLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Price,CostPrice,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Policy = "deletepolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        public IActionResult Category()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Category([Bind("CategoryName,CategoryDescription")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(productCategory);
        }
        [HttpGet]
        public IActionResult ProductActivities()
        {
          
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(t => t.ProductName), "ProductId", "ProductName");
            return View();
        }


 
       

        public IActionResult LLL(int ProductName, DateTime? fromDate, DateTime? toDate)
        {
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            ViewBag.ProductName = _context.Products.Where(o => o.ProductId == ProductName).Select(u => u.ProductName).FirstOrDefault();
            ViewBag.Totalkg = _context.WholesaleOrderedItems.Include(p => p.Order).Where(o => o.ProductId == ProductName && o.Order.OrderDate >= fromDate && o.Order.OrderDate <= toDate).Sum(p =>p.Quantity);

            var allOrders = _context.WholesaleOrderedItems.Include(p => p.Order).Where(t => t.Order.OrderDate >= fromDate && t.Order.OrderDate <= toDate && t.ProductId == ProductName).OrderByDescending(m => m.OrderId);

            

            LLLVM model = new LLLVM();
            model.orderedItems = _context.WholesaleOrderedItems.Include(p => p.Order).Where(t => t.Order.OrderDate >= fromDate && t.Order.OrderDate <= toDate && t.ProductId == ProductName).OrderByDescending(m => m.OrderId);
            model.OtherDebits = _context.OtherDebits.Include(p => p.product).Include(p => p.debitMode).Where(t => t.DebitDate >= fromDate && t.DebitDate <= toDate && t.ProductId == ProductName).OrderByDescending(m => m.DebitDate);
            model.NewStockLists = _context.NewStockLists.Include(p => p.Product).Where(t => t.NewOrderDate >= fromDate && t.NewOrderDate <= toDate && t.ProductId == ProductName).OrderByDescending(m => m.NewOrderDate);

            return View(model);

        }
    }
}
