using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using StockMVC.Data;
using StockMVC.Models;
using Microsoft.AspNetCore.Authorization;
using StockMVC.ViewModel;

namespace StockMVC.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            var model = new Customer { RegisterDate = DateTime.Now };
            return View(model);
        }

        public IActionResult AddPrice(int? id)
        {
          
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(m => m.ProductName), "ProductId", "ProductName");
            ViewBag.CustomerId = id;

            return View();
        }

        [HttpPost]
        [Route("/Customers/AddUpdatePrice")]
        public JsonResult AddUpdatePrice(WholesalePrices[] info)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in info)
                {
                    _context.Entry(item).State = !_context.WholesalePrices.Any(f => f.CustomerId
                    == item.CustomerId && f.ProductId == item.ProductId ) ? EntityState.Added : EntityState.Modified;
                    _context.SaveChanges();
                }
                //foreach (var lis in tests)
                //{
                //    _context.NewStockLists.Add(lis);
                //}

                //foreach (var item in info)
                //{
                //    StockLevel updatedstock = (from c in _context.StockLevels
                //                               where c.ProductId == item.ProductId
                //                               select c).FirstOrDefault();
                //    updatedstock.Balance += item.Balance;
                //}

                _context.SaveChanges();
                return Json("Prices Updated/Added");

            }
            return Json(new { newUrl = Url.Action("Index", "Order") });
        }

   


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CustomerName,Address,Email,Mobile,RegisterDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        //[Authorize(Policy = "editpolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult CustomerPrices(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewBag.CustomerId = id;


            var order = (from oin in _context.WholesalePrices
                         join pid in _context.Products
                         on oin.ProductId equals pid.ProductId
                         join cus in _context.Customers
                         on oin.CustomerId equals cus.CustomerID
                         where oin.CustomerId == id
                         select new
                         {
                             ProductName = pid.ProductName,
                             Price = oin.Price,
                            
                         });



            ViewBag.CustomerName = _context.Customers.Where(o => o.CustomerID == id).Select(u => u.CustomerName).FirstOrDefault();


            List<CustomersPricesVM> detailsViewModel = new List<CustomersPricesVM>();
            foreach (var odered in order)
            {
                CustomersPricesVM data = new CustomersPricesVM();
                data.ProductName = odered.ProductName;
                data.Price = odered.Price;
                

                detailsViewModel.Add(data);
            }



            if (order == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,CustomerName,Address,Email,Mobile,RegisterDate")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Policy = "deletepolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
