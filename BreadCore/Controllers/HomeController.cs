using BreadCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System;

namespace BreadCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Medewerker()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string bedienerNr, string wachtwoord, string returnUrl)
        {
            SqlConnection conn = new SqlConnection("Server=.;Database=Bread;Trusted_Connection=True");
            SqlCommand verify = new SqlCommand();
            SqlDataReader dr;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            verify.Connection = conn;
            conn.Open();

            verify.CommandText = "SELECT * FROM [Medewerker] WHERE BedienerNr ='" + bedienerNr + "' AND Wachtwoord = '" + wachtwoord + "'";
            dr = verify.ExecuteReader(CommandBehavior.SingleResult);


            if (dr.HasRows)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("bedienerNr", bedienerNr));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, bedienerNr));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                int bedienerNrInt = Int32.Parse(bedienerNr);
                if (bedienerNrInt <= 100)
                {
                    return View("Medewerker");
                }
                if (bedienerNrInt >= 101 && bedienerNrInt <= 999)
                {
                    return View("Manager");
                }
                else
                {
                    return View("SysteemBeheerder");
                }
                conn.Close();
            }
            else
            {
                TempData["Error"] = "Error. BedienerNr of Wachtwoord is fout.";
                return View("login");
                conn.Close();
            }
        }



        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
