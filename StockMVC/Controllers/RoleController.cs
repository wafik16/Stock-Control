using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
      RoleManager<IdentityRole> roleManager;

        public RoleController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.roleManager = roleManager;
        }
        // GET: RoleController
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles.ToList());
        }


        // GET: RoleController/Create
        [Authorize(Policy = "allowpolicy")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
