using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspMvcCoreHostTest.Models;
using aspMvcCoreHostTest.ViewModels;

namespace aspMvcCoreHostTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        List<Phone> phones;

        List<Company> companies;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Company apple = new Company { Id = 1, Name = "Apple", Country = "USA" };
            Company samsung = new Company { Id = 2, Name = "Samsung", Country = "Korea" };
            Company google = new Company { Id = 3, Name = "Google", Country = "USA" };

            companies = new List<Company>();

            companies.Add(apple);
            companies.Add(samsung);
            companies.Add(google);

            phones = new List<Phone>
            {
                new Phone { Id = 1, Manufacturer = apple, Name ="iPhone X" , Price = 56000},
                new Phone { Id = 2, Manufacturer = apple, Name ="iPhone XZ" , Price = 56000},
                new Phone { Id = 3, Manufacturer = samsung, Name ="Galaxy 9" , Price = 56000},
                new Phone { Id = 4, Manufacturer = samsung, Name ="Galaxy 10" , Price = 56000},
                new Phone { Id = 5, Manufacturer = google, Name ="Pixel 2" , Price = 56000},
                new Phone { Id = 6, Manufacturer = google, Name ="Pixel XL" , Price = 56000},
            };
        }

        public IActionResult Index(int? companyId)
        {
            if (ModelState.IsValid)
            {

            }

            List<CompanyModel> compModels = companies.Select(c => new CompanyModel
            { Id = c.Id, Name = c.Name }).ToList();

            compModels.Insert(0, new CompanyModel { Id = 0, Name = "All" });

            IndexViewModel ivm = new IndexViewModel
            {
                Companies = compModels,
                Phones = phones
            };

            if (companyId != null && companyId > 0)
            {
                ivm.Phones = phones.Where(p => p.Manufacturer.Id == companyId);
            }

            return View(ivm);
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
