using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GetPatientAPI.Models;
using System.Data.SqlClient;

namespace GetPatientAPI.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public List<Patient> GetPatient()
        {
            List<Patient> patients = new List<Patient>();
            //--SQLConnection
            using (SqlConnection connection = new SqlConnection("Data Source=192.168.37.242;Initial Catalog=Falcon;User ID=data_exchange;Password=swapit;Timeout=200"))
            {
                string sql = $"Select top 10 * from Falcon.Patient";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Patient pat = new Patient();
                            pat.IdPatient = Convert.ToInt32(dataReader["IdPatient"]);
                            pat.NameLast = Convert.ToString(dataReader["NameLast"]);
                            pat.NameFirst = Convert.ToString(dataReader["NameFirst"]);
                            pat.NameMiddle = Convert.ToString(dataReader["NameMiddle"]);
                            patients.Add(pat);
                        }
                    }
                    connection.Close();
                }
            }
            return patients;
        }
    }
}
