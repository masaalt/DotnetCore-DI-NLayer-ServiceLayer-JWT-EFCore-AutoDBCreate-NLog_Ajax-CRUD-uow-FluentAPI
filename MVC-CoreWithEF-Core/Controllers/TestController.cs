using DB.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceLayer.Employee;
using System.Linq;
namespace MVC_CoreWithEF_Core.Controllers
{
    public class TestController : Controller
    {


        private readonly ILogger<TestController> _logger;

        private readonly IConfiguration _config;
        private IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;
        private readonly MVCEFCoreContext _context;


        public TestController(MVCEFCoreContext context, IConfiguration config, IEmployeeService employeeService, ITokenService tokenService, ILogger<TestController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
            _tokenService = tokenService;
            _config = config;
            _context = context;
        }

        public IActionResult Index()
        {
            MVCEFCoreContext db = new MVCEFCoreContext();
            return View();
        }

        [HttpGet]
        public IActionResult getPopulation()
        {
            MVCEFCoreContext db = new MVCEFCoreContext();
            return Json(db.Employees.ToList());
        }

        [HttpPost]
        public IActionResult DeleteEmp([FromBody] Employees employee)
        {
            MVCEFCoreContext db = new MVCEFCoreContext();
            db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(employee);
        }

        [HttpPost]
        public IActionResult UpdateEmp([FromBody] Employees employee)
        {
            MVCEFCoreContext db = new MVCEFCoreContext();
            db.Employees.Update(employee);
            db.SaveChanges();
            return Json(employee);
        }

        [ServiceFilter(typeof(ClientIpCheckActionFilter))]
        public IActionResult AddEmployee()
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                ViewBag.Message = "No Token";
                return (RedirectToAction("Index"));
            }
            if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                ViewBag.Message = "Invalid Token";
                return (RedirectToAction("Index"));
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employees data)
        {
            bool added = _employeeService.CreateEmployee(data);
            if (added)
            {
                _logger.LogWarning($"Employee Inserted.");
            }
            else
            {
                _logger.LogWarning($"Employee can't be Inserted.");
            }
            return RedirectToAction("AddEmployee");
           
        }

    }
}