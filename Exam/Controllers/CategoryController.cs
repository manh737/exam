using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.DBContexts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam.Controllers
{
    public class CategoryController : Controller
    {
        private MyDBContext myDbContext;

        public CategoryController(MyDBContext context)
        {
            myDbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = this.myDbContext.Categorys.ToList();
            return View();
        }
    }
}
