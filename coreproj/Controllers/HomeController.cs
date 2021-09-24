using coreproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace coreproj.Controllers
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
            List<Person> persons = new List<Person>();

            using (MySqlConnection con = new MySqlConnection("Server=alpandisdemoserver.mysql.database.azure.com; Port=3306; Database=crud; Uid=alpandis@alpandisdemoserver; Pwd=FafaAl22; SslMode=Preferred;"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from student", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person();
                    person.SID = Convert.ToInt32(reader["SID"]);
                    person.Name = reader["Name"].ToString();
                    person.Address = reader["Address"].ToString();
                    person.Email = reader["Email"].ToString();
                    person.Mobile = Convert.ToInt32(reader["Mobile"]);
                    ;

                    persons.Add(person);
                }
                reader.Close();
            }
                return View(persons);
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
