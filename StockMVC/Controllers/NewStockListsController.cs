using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockMVC.Data;
using StockMVC.Models;
using StockMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace StockMVC.Controllers
{
    [Authorize]
    public class NewStockListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewStockListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Stock()
        {
            var applicationDbContext = _context.StockLevels.Include(p => p.Product).OrderBy(m => m.Product.ProductName);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> StockTake()
        {
            var applicationDbContext = _context.StockLevels.Include(p => p.Product).OrderBy(m => m.Product.ProductName).ThenBy(m => m.Product.CategoryId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NewStockLists
        public IActionResult ListStock()
        {
            var order = _context.NewStockLists.OrderByDescending(x => x.Id).GroupBy(x => x.InvoiceNumber).Select(y => y.First()).Distinct();
            //var order = (from oin in _context.NewStockLists

            //             select new
            //             {
            //                 Date = oin.NewOrderDate,
            //                 Invoice = oin.InvoiceNumber,
            //             });
            List<NewStockLists> newstk = new List<NewStockLists>();
            foreach (var odered in order)
            {
                NewStockLists data = new NewStockLists();
                data.InvoiceNumber = odered.InvoiceNumber;
                data.NewOrderDate = odered.NewOrderDate;

                newstk.Add(data);
            }

            return View(newstk);
        }

        


        [HttpGet]
        [Authorize(Policy = "writepolicy")]
        // GET: NewStockLists/StockLevel
        public IActionResult Create()
        {
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(m => m.ProductName), "ProductId", "ProductName");
            if(_context.NewStockLists.OrderByDescending(t => t.Id).Select(u => u.Id).FirstOrDefault() == null)
            {
                ViewBag.LastInvoiceNumber = 0;
            } else
            {
                ViewBag.LastInvoiceNumber = _context.NewStockLists.OrderByDescending(t => t.Id).Select(u => u.Id).FirstOrDefault();
            }
            

            return View();
        }

        [HttpGet]
        public IActionResult ListDetail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.date = _context.NewStockLists.Where(u => u.InvoiceNumber == id).Select(u => u.NewOrderDate).FirstOrDefault();
            ViewBag.total = _context.NewStockLists.Where(u => u.InvoiceNumber == id).Select(u => u.NewOrderDate).FirstOrDefault();
            ViewBag.sumtotal = _context.NewStockLists.Include(p => p.Product).Where(u => u.InvoiceNumber == id).Sum(i => i.Quantity * i.Product.CostPrice);
            ViewBag.Inv = id;
            var order = (from oin in _context.NewStockLists
                         join pid in _context.Products
                         on oin.ProductId equals pid.ProductId
                         where oin.InvoiceNumber == id
                         select new
                         {
                             ProductName = pid.ProductName,
                             InvoiceNo = oin.InvoiceNumber,
                             Date = oin.NewOrderDate,
                             Quantity = oin.Quantity,
                             CostPrice = pid.CostPrice,
                             Total = pid.CostPrice * oin.Quantity
                         });
            List<ListDetails> detailsViewModel = new List<ListDetails>();
            foreach (var odered in order)
            {
                ListDetails data = new ListDetails();
                data.ProductName = odered.ProductName;
                data.InvoiceNumber = odered.InvoiceNo;
                data.Quantity = odered.Quantity;
                data.Date = odered.Date;
                data.CostPrice = odered.CostPrice;
                data.Total = odered.Total;

                detailsViewModel.Add(data);
            }


            if (order == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        [HttpPost]
        [Route("/Orders/StockLevel")]
        public JsonResult StockLevel(NewStockLists[] tests, StockLevel[] info)
        {
            if (ModelState.IsValid)
            {
                foreach (var lis in tests)
                {
                    _context.NewStockLists.Add(lis);
                }

                foreach (var item in info)
                {
                    StockLevel updatedstock = (from c in _context.StockLevels
                                               where c.ProductId == item.ProductId
                                               select c).FirstOrDefault();
                    updatedstock.Balance += item.Balance;
                }

                _context.SaveChanges();
                return Json("Order Successfully Placed");

            }
            return Json(new { newUrl = Url.Action("Index", "Order") });
        }

        [Authorize(Policy = "deletepolicy")]
        public IActionResult Deleted(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.NewStockLists
                .FirstOrDefault(m => m.InvoiceNumber == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("Deleted")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletedConfirmed(string id)
        {
            var order = (from oin in _context.NewStockLists
                         where oin.InvoiceNumber == id
                         select new
                         {
                             ProductName = oin.ProductId,
                             Qty = oin.Quantity,
                         });
            foreach (var item in order)
            {
                StockLevel updatedstock = (from c in _context.StockLevels
                                           where c.ProductId == item.ProductName
                                           select c).FirstOrDefault();
                updatedstock.Balance -= item.Qty;
            }

            _context.NewStockLists.RemoveRange(_context.NewStockLists.Where(x => x.InvoiceNumber == id));

            _context.SaveChanges();
            return RedirectToAction(nameof(ListStock));
        }

        private bool OrderExisted(DateTime id)
        {
            return _context.NewStockLists.Any(e => e.NewOrderDate == id);
        }

       
    }
}
