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
using PagedList;

namespace StockMVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var applicationDbContext = _context.orders.Include(p => p.PaymentMode).OrderByDescending(m => m.OrderId).ThenBy(m => m.OrderDate);

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = (IOrderedQueryable<Order>)applicationDbContext.Include(p => p.PaymentMode).Where(s => s.PaymentMode.ModeOfPayment.Contains(searchString));
            }

            int pageSize = 7;
            return View(await PaginatedList<Order>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));


        }

        public async Task<IActionResult> WholesaleIndex(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var applicationDbContext = _context.WholesaleOrders.Include(c => c.customer).Include(p => p.PaymentMode).OrderByDescending(m => m.OrderId).ThenBy(m => m.OrderDate);

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = (IOrderedQueryable<WholesaleOrder>)applicationDbContext.Include(p => p.PaymentMode).Where(s => s.PaymentMode.ModeOfPayment.Contains(searchString));
            }

            int pageSize = 7;
            return View(await PaginatedList<WholesaleOrder>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));


        }

        public IActionResult Lists(DateTime? fromDate, DateTime? toDate)
        {
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
           

           var allOrders = _context.orders.Where(t => t.OrderDate >= fromDate && t.OrderDate <= toDate).OrderByDescending(m => m.OrderId);
            return View(allOrders.ToList() );
           
        }


        public async Task<IActionResult> OtherDebitList()
        {
            var san = _context.OtherDebits.Include(p => p.product).Include(p => p.debitMode).OrderByDescending(m => m.DebitDate);
            return View(await san.ToListAsync());
        }




        public IActionResult Report(DateTime? fromDate, DateTime? toDate)
        {
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            var link = (from ord in _context.orders

                        where ord.OrderDate >= fromDate && ord.OrderDate <= toDate
                        select new
                        {

                            OrderId = ord.OrderId,
                            Invoice = ord.InvoiceNumber,
                            Date = ord.OrderDate,
                            Discount = ord.Discount,
                            Delivery = ord.DeliveryFee,
                            TotalWithoutVat = ord.TotalWithoutVat,
                            TotalVat = ord.TotalVat,
                            TotalAmount = ord.TotalAmount
                        });

            var order = (from oin in _context.orderedItems
                         join pid in _context.Products
                         on oin.ProductId equals pid.ProductId
                         join ord in _context.orders
                         on oin.OrderId equals ord.OrderId
                         where ord.OrderDate >= fromDate && ord.OrderDate <= toDate
                         select new
                         {
                             ProductName = pid.ProductName,
                             Price = oin.Price,
                             Total = oin.Total,
                             Quantity = oin.Quantity,
                             OrderId = ord.OrderId,
                             Invoice = ord.InvoiceNumber,
                             Date = ord.OrderDate,
                             Discount = ord.Discount,
                             Delivery = ord.DeliveryFee,
                             TotalWithoutVat = ord.TotalWithoutVat,
                             TotalVat = ord.TotalVat,
                             TotalAmount = ord.TotalAmount
                         });


            List<ReportModel> reportModel = new List<ReportModel>();
            foreach (var odered in link)
            {
                ReportModel data = new ReportModel();
                
                data.DeliveryFee = odered.Delivery;
                data.Discount = odered.Discount;
                data.InvoiceNumber = odered.Invoice;
                data.OrderDate = odered.Date;
                data.OrderId = odered.OrderId;
                data.TotalAmount = odered.TotalAmount;
                data.TotalVat = odered.TotalVat;
                data.TotalWithoutVat = odered.TotalWithoutVat;

                foreach(var ink in order)
                {
                    data.ProductName = ink.ProductName;
                    data.Price = ink.Price;
                    data.Quantity = ink.Quantity;
                    data.Total = ink.Total;
                }

                reportModel.Add(data);
            }

           

            if (order == null)
            {
                return NotFound();
            }

            return View(reportModel);

        }




        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            


            var order = (from oin in _context.orderedItems
                                         join pid in _context.Products
                                         on oin.ProductId equals pid.ProductId
                                         where oin.OrderId == id
                                         select new
                                         {
                                             ProductName = pid.ProductName,
                                             Price = oin.Price,
                                             Total = oin.Total,
                                             Quantity = oin.Quantity
                                         });

           

            ViewBag.InvoiceNumber = _context.orders.Where(o => o.OrderId == id).Select(u => u.InvoiceNumber).FirstOrDefault();
            ViewBag.OrderDate = _context.orders.Where(o => o.OrderId == id).Select(u => u.OrderDate).FirstOrDefault();
            ViewBag.Discount = _context.orders.Where(o => o.OrderId == id).Select(u => u.Discount).FirstOrDefault();
            ViewBag.DeliveryFee = _context.orders.Where(o => o.OrderId == id).Select(u => u.DeliveryFee).FirstOrDefault();
            ViewBag.TotalAmount = _context.orders.Where(o => o.OrderId == id).Select(u => u.TotalAmount).FirstOrDefault();
            ViewBag.TotalVat = _context.orders.Where(o => o.OrderId == id).Select(u => u.TotalVat).FirstOrDefault();

            List<DetailsViewModel> detailsViewModel = new List<DetailsViewModel>();
            foreach (var odered in order)
            {
                DetailsViewModel data = new DetailsViewModel();
                data.ProductName = odered.ProductName;
                data.Price = odered.Price;
                data.Quantity = odered.Quantity;
                data.Total = odered.Total;

                detailsViewModel.Add(data);
            }

           

            if (order == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        public IActionResult WholesaleDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }




            var order = (from oin in _context.WholesaleOrderedItems
                         join pid in _context.Products
                         on oin.ProductId equals pid.ProductId
                         where oin.OrderId == id
                         select new
                         {
                             ProductName = pid.ProductName,
                             Price = oin.Price,
                             Total = oin.Total,
                             Quantity = oin.Quantity
                         });



            ViewBag.InvoiceNumber = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.InvoiceNumber).FirstOrDefault();
            ViewBag.OrderDate = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.OrderDate).FirstOrDefault();
            ViewBag.Discount = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.Discount).FirstOrDefault();
            ViewBag.DeliveryFee = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.DeliveryFee).FirstOrDefault();
            ViewBag.TotalAmount = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.TotalAmount).FirstOrDefault();
            ViewBag.TotalVat = _context.WholesaleOrders.Where(o => o.OrderId == id).Select(u => u.TotalVat).FirstOrDefault();

            List<DetailsViewModel> detailsViewModel = new List<DetailsViewModel>();
            foreach (var odered in order)
            {
                DetailsViewModel data = new DetailsViewModel();
                data.ProductName = odered.ProductName;
                data.Price = odered.Price;
                data.Quantity = odered.Quantity;
                data.Total = odered.Total;

                detailsViewModel.Add(data);
            }



            if (order == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        [Authorize(Policy = "writepolicy")]
        [HttpGet]
        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerName"] = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(t => t.ProductName), "ProductId", "ProductName");
            ViewData["Price"] = new SelectList(_context.Products, "ProductId", "Price");
            ViewData["PaymentMode"] = new SelectList(_context.paymentModes, "PaymentModeId", "ModeOfPayment");
            ViewBag.UserId = User.Identity.Name;
            if(_context.orders.OrderByDescending(t => t.OrderId).Select(u => u.OrderId).FirstOrDefault() == null)
            {
                ViewBag.LastInvoiceNumber = 0;
            } else
            {
                ViewBag.LastInvoiceNumber = _context.orders.OrderByDescending(t => t.OrderId).Select(u => u.OrderId).FirstOrDefault();
            }
            
            
            return View();
        }

        [Authorize(Policy = "writepolicy")]
        [HttpGet]
        // GET: Orders/Create
        public IActionResult WholesaleCreate()
        {
            ViewData["CustomerName"] = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(t => t.ProductName), "ProductId", "ProductName");
            ViewData["Price"] = new SelectList(_context.Products, "ProductId", "Price");
            ViewData["PaymentMode"] = new SelectList(_context.paymentModes, "PaymentModeId", "ModeOfPayment");
            ViewBag.UserId = User.Identity.Name;
            if (_context.orders.OrderByDescending(t => t.OrderId).Select(u => u.OrderId).FirstOrDefault() == null)
            {
                ViewBag.LastInvoiceNumber = 0;
            }
            else
            {
                ViewBag.LastInvoiceNumber = _context.orders.OrderByDescending(t => t.OrderId).Select(u => u.OrderId).FirstOrDefault();
            }


            return View();
        }


        [Authorize(Policy = "writepolicy")]

        [HttpGet]
        // GET: Orders/Create
        public IActionResult CreateOtherDebit()
        {
            ViewData["ProductName"] = new SelectList(_context.Products.OrderBy(t => t.ProductName), "ProductId", "ProductName");
           // ViewData["Price"] = new SelectList(_context.Products, "ProductId", "Price");
            ViewData["Debitmode"] = new SelectList(_context.DebitModes, "DebitModeId", "ModeOfDebit");
            ViewBag.UserId = User.Identity.Name;
            var UserName = User.Identity.Name;
            var UserId = _context.Users.Where(u => u.UserName == UserName).Select(u => u.Id).FirstOrDefault();

            var roleId = _context.UserRoles.Where(u => u.UserId == UserId).Select(u => u.RoleId).FirstOrDefault();
            ViewBag.RoleName = _context.Roles.Where(u => u.Id == roleId).Select(u => u.Name).FirstOrDefault();

            return View();
        }

        [HttpGet]
        public JsonResult GetProductUnitPrice(int ItemId)
        {
            decimal UnitPrice =_context.Products.Where(u => u.ProductId == ItemId).Select(u => u.Price).SingleOrDefault();
            decimal StockBalance = _context.StockLevels.Where(u => u.ProductId == ItemId).Select(u => u.Balance).SingleOrDefault();
            var del = "";
            var myt = "";
            del = UnitPrice.ToString();
            myt = StockBalance.ToString();

            List<MyList> lyt = new List<MyList>();
           MyList abc = new MyList() { 
           UnitPrice = 1,
           StockBalance = 2
           };

            
            return Json(new { del, myt});
            //return Json( new { UnitPrice, StockBalance });

        }

        [HttpGet]
        public JsonResult GetCustomerProductUnitPrice(int ItemId, int CustomerId)
        {
            decimal UnitPrice = _context.WholesalePrices
                .Where(u => u.ProductId == ItemId && u.CustomerId == CustomerId)
                .Select(u => u.Price).SingleOrDefault();
            decimal StockBalance = _context.wholesaleStockLevels.Where(u => u.ProductId == ItemId).Select(u => u.Balance).SingleOrDefault();
            var del = "";
            var myt = "";
            del = UnitPrice.ToString();
            myt = StockBalance.ToString();

            List<MyList> lyt = new List<MyList>();
            MyList abc = new MyList()
            {
                UnitPrice = 1,
                StockBalance = 2
            };


            return Json(new { del, myt });
            //return Json( new { UnitPrice, StockBalance });

        }


        [HttpGet]
        public JsonResult GetCustomerProducts(int CustomerId)
        {
        
            return Json(_context.WholesalePrices.Include(p => p.customer)
                .Where(u => u.CustomerId == CustomerId)
                .Select(x => new
            {
                ProductId = x.ProductId,
                ProductName = x.product.ProductName
            }).ToList());


        }


        //POST: Orders/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("/Orders/Create")]
        public JsonResult Create(Order order, OrderedItem[] orderitems, StockLevel[] stockLevels)
        {
            if (ModelState.IsValid)
            {
               


                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        
                        _context.Add(order);
                        foreach (var item in orderitems)
                        {
                           
                                item.OrderId = order.OrderId;

                                _context.orderedItems.Add(item);

                           
                        }

                        foreach (var item in stockLevels)
                        {

                            StockLevel updatedstock = (from c in _context.StockLevels
                                                       where c.ProductId == item.ProductId
                                                       select c).FirstOrDefault();
                            updatedstock.Balance -= item.Balance;
                        }

                        _context.SaveChanges();
                        foreach (var item in _context.StockLevels)
                        {
                            if( item.Balance < 0)
                            {
                                throw new InvalidOperationException();
                            }
                            
                        }



                        transaction.Commit();
                       




                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }

                    
                }

                return Json("Order Successfully Placed");



            }
            return Json(new { newUrl = Url.Action("Index", "Order") });
        }

        [HttpPost]
        [Route("/Orders/WholesaleCreate")]
        public JsonResult wholesaleCreate(WholesaleOrder order, WholesaleOrderedItem[] orderitems, WholesaleStockLevel[] stockLevels)
        {
            if (ModelState.IsValid)
            {

                //_context.Add(order);
                //_context.SaveChanges();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {


                        _context.Add(order);
                        foreach (var item in orderitems)
                        {

                            item.OrderId = order.OrderId;

                            _context.WholesaleOrderedItems.Add(item);


                        }

                        foreach (var item in stockLevels)
                        {

                            WholesaleStockLevel updatedstock = (from c in _context.wholesaleStockLevels
                                                       where c.ProductId == item.ProductId
                                                       select c).FirstOrDefault();
                            updatedstock.Balance -= item.Balance;
                        }

                        _context.SaveChanges();
                        foreach (var item in _context.wholesaleStockLevels)
                        {
                            if (item.Balance < 0)
                            {
                                throw new InvalidOperationException();
                            }

                        }



                        transaction.Commit();





                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }


                }

                return Json("Order Successfully Placed");



            }
            return Json(new { newUrl = Url.Action("Index", "Order") });
        }




        [Authorize(Policy = "deletepolicy")]

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders.Include(p =>p.PaymentMode)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> WholesaleDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.WholesaleOrders.Include(p => p.PaymentMode)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, OrderedItem[] orderitems, StockLevel[] stockLevels)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var lorder = (from oin in _context.orderedItems.Include(p => p.Order)
                                  where oin.OrderId == id
                                  select new
                                  {
                                      ProductId = oin.ProductId,
                                      Qty = oin.Quantity,
                                  });

                    foreach (var item in lorder)
                    {

                        StockLevel updatedstock = (from c in _context.StockLevels
                                                   where c.ProductId == item.ProductId
                                                   select c).FirstOrDefault();
                        updatedstock.Balance += item.Qty;
                    }


                    var order = await _context.orders.FindAsync(id);
                    _context.orders.Remove(order);



                    foreach (var item in orderitems)
                    {

                        item.OrderId = order.OrderId;

                        _context.orderedItems.Remove(item);

                    }

                    

                    _context.SaveChanges();


                    transaction.Commit();





                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }


            }

           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("WholesaleDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WholesaleDeleteConfirmed(int id, WholesaleOrderedItem[] orderitems)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var lorder = (from oin in _context.WholesaleOrderedItems.Include(p => p.Order)
                                  where oin.OrderId == id
                                  select new
                                  {
                                      ProductId = oin.ProductId,
                                      Qty = oin.Quantity,
                                  });

                    foreach (var item in lorder)
                    {

                        WholesaleStockLevel updatedstock = (from c in _context.wholesaleStockLevels
                                                   where c.ProductId == item.ProductId
                                                   select c).FirstOrDefault();
                        updatedstock.Balance += item.Qty;
                    }


                    var order = await _context.WholesaleOrders.FindAsync(id);
                    _context.WholesaleOrders.Remove(order);



                    foreach (var item in orderitems)
                    {

                        item.OrderId = order.OrderId;

                        _context.WholesaleOrderedItems.Remove(item);

                    }



                    _context.SaveChanges();


                    transaction.Commit();





                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }


            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WholesaleIndex));
        }



        private bool OrderExists(int id)
        {
            return _context.orders.Any(e => e.OrderId == id);
        }

        private bool WholesaleOrderExists(int id)
        {
            return _context.WholesaleOrders.Any(e => e.OrderId == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult CreateOtherDebit( CreateOtherDebitVM product, StockLevel stockLevel, WholesaleStockLevel wholesaleStockLevel)
        {
         
            
            if (ModelState.IsValid)
            {

                if (product.RoleName == "Retail")
                {
                    decimal order = (from oin in _context.Products
                                     where oin.ProductId == product.ProductId
                                     select oin.Price).FirstOrDefault();


                    OtherDebit oth = new OtherDebit();
                    oth.DebitDate = product.DebitDate;
                    oth.DebitModeId = product.DebitModeId;
                    oth.TotalAmount = order * product.Quantity;



                    oth.Quantity = product.Quantity;
                    oth.ProductId = product.ProductId;
                    oth.UserId = User.Identity.Name;

                    _context.OtherDebits.Add(oth);

                    if (product.DebitModeId == 4)
                    {
                        StockLevel upd = (from c in _context.StockLevels
                                          where c.ProductId == product.ProductId
                                          select c).FirstOrDefault();
                        upd.Balance += product.Quantity;
                    }
                    else
                    {
                        StockLevel updatedstock = (from c in _context.StockLevels
                                                   where c.ProductId == product.ProductId
                                                   select c).FirstOrDefault();
                        updatedstock.Balance -= product.Quantity;
                    }

                    _context.SaveChanges();
                }
                else
                {
                    decimal order = (from oin in _context.Products
                                     where oin.ProductId == product.ProductId
                                     select oin.Price).FirstOrDefault();


                    OtherDebit oth = new OtherDebit();
                    oth.DebitDate = product.DebitDate;
                    oth.DebitModeId = product.DebitModeId;
                    oth.TotalAmount = order * product.Quantity;



                    oth.Quantity = product.Quantity;
                    oth.ProductId = product.ProductId;
                    oth.UserId = User.Identity.Name;

                    _context.OtherDebits.Add(oth);

                    if (product.DebitModeId == 4)
                    {
                        WholesaleStockLevel upd = (from c in _context.wholesaleStockLevels
                                          where c.ProductId == product.ProductId
                                          select c).FirstOrDefault();
                        upd.Balance += product.Quantity;
                    }
                    else
                    {
                        WholesaleStockLevel updatedstock = (from c in _context.wholesaleStockLevels
                                                   where c.ProductId == product.ProductId
                                                   select c).FirstOrDefault();
                        updatedstock.Balance -= product.Quantity;
                    }

                    _context.SaveChanges();

                }
            }
            return RedirectToAction(nameof(OtherDebitList));
        }

        
        public IActionResult List(DateTime? fromDate, DateTime? toDate)
        {
           
                ViewData["fromDate"] = fromDate;
                ViewData["toDate"] = toDate;
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                ViewBag.totalamount = _context.orders
                    .Where(t => t.OrderDate >= fromDate && t.OrderDate <= toDate).Sum(i => i.TotalAmount);

                var order = _context.orderedItems.Include(p => p.Order).Include(p => p.Product)
                    .Where(t => t.Order.OrderDate >= fromDate && t.Order.OrderDate <= toDate)
                    .OrderBy(x => x.Order.OrderDate);
                return View(order);

            

            
        }

        public IActionResult WholesaleLists(DateTime? fromDate, DateTime? toDate)
        {
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            ViewBag.totalamount = _context.WholesaleOrders
                .Where(t => t.OrderDate >= fromDate && t.OrderDate <= toDate).Sum(i => i.TotalAmount);

            var order = _context.WholesaleOrderedItems.Include(p => p.Order).Include(p => p.Product).Include(p => p.Order.customer)
                .Where(t => t.Order.OrderDate >= fromDate && t.Order.OrderDate <= toDate)
                .OrderBy(x => x.Order.OrderDate);
            return View(order);

        }
        [HttpGet]
        public IActionResult WholesaleReturns(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ProductName"] = new SelectList(_context.WholesaleOrders.Include(m => m.Product).Where(m => m.OrderId == id), "ProductId", "ProductName");
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

    }
}
