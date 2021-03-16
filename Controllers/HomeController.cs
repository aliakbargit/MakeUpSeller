using MakeUpSeller.Data;
using MakeUpSeller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MakeUpSeller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var orders = _context.OrderItems.ToList();
            var completeds = _context.Completeds.ToList();

            IndexPageViewModel viewModel = new IndexPageViewModel { Products =products,OrderItems = orders,Competeds= completeds };

            return View(viewModel);
        }

        public IActionResult AddToOderItem(int Id) {
            var product = _context.Products.FirstOrDefault(p=>p.Id == Id);
            OrderItem order = new() { Name = product.Name, Price = product.Price };
            _context.Set<OrderItem>().Add(order);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Confirm() {
            List<OrderItem> orderItems = _context.OrderItems.ToList();
            List<Completed> completeds = new List<Completed>();
            foreach (var item in orderItems) {
                var itemC = new Completed { Name = item.Name,Price = item.Price };
                completeds.Add(itemC);
            }
            _context.OrderItems.RemoveRange(orderItems);
            _context.Completeds.AddRange(completeds);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Cancel() {
            _context.OrderItems.RemoveRange(_context.OrderItems);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult RemoveFromComplete(int Id) {
            var item = _context.Completeds.FirstOrDefault(c=>c.Id == Id);
            _context.Completeds.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
             

        public IActionResult ClearAll() {
            _context.Completeds.RemoveRange(_context.Completeds);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
