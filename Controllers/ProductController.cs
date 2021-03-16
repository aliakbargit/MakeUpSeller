using MakeUpSeller.Data;
using MakeUpSeller.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeUpSeller.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _context.Set<Product>().OrderBy(p => p.Name).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
          return RedirectToAction("index"); 
        }

        public IActionResult Edit(int Id) {
            Product product = _context.Set<Product>().FirstOrDefault(p=>p.Id == Id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            _context.Set<Product>().Update(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Delete(int Id)
        {
            var item = _context.Set<Product>().FirstOrDefault(p => p.Id == Id);
            _context.Set<Product>().Remove(item);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
