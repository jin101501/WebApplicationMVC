using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationMVC.Models;
using MYDB1.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MYDB1Context _mydb1;

        public HomeController(ILogger<HomeController> logger, MYDB1Context mydb1)
        {
            this._logger = logger;
            this._mydb1 = mydb1;
        }

        public IActionResult Index()
        {            
            return View();
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

        [HttpGet]
        [Route("Home/SelectStudent")]
        public async Task<IActionResult> SelectStudent()
        {
            IActionResult result = BadRequest();

            try
            {
                var dataList = await _mydb1.Student.ToListAsync().ConfigureAwait(false);

                result = new JsonResult(dataList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result = BadRequest(ex);
            }

            return result;
        }
    }
}