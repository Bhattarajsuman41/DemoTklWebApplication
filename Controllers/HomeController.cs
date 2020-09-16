using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoTklWebApplication.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using PagedList;

namespace DemoTklWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(int? page)
        {

            string Baserul = "https://f3fb9c40-bc5f-4dab-84dd-5c935c260522.mock.pstmn.io/employee";
            reviewEmployee review = new reviewEmployee();
            List<employee> emp = new List<employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baserul);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(Baserul);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list 
                    var e = JsonConvert.DeserializeObject(Response);
                    var result = JsonConvert.DeserializeObject<List<employee>>(e.ToString());

                    var averageAge = result.Average(x => x.employee_age);
                    var averageSalary = result.Average(x => x.employee_salary);
                    review.averageAge = Convert.ToDouble(averageAge);
                    review.averageSalary = Convert.ToDouble(averageSalary);

                    foreach (var item in result)
                    {
                        var averageagestatus = "";
                        var averageSalarystatus = "";
                        var statusColor = "";
                        var AgeColor = "";
                        

                        if (item.employee_age > averageAge)
                        {
                            averageagestatus = "Above";
                            statusColor = "#ff1493";

                        }
                        else if (item.employee_age < averageAge)
                        {
                            averageagestatus = "Below";
                            statusColor = "#00ff00";
                        }
                        else
                        {
                            averageagestatus = "Same";
                            statusColor = "#ffff00";
                        }


                        if (item.employee_salary > averageSalary)
                        {
                            averageSalarystatus = "Above";
                            AgeColor = "#ff1493";
                        }
                        else if (item.employee_salary < averageSalary)
                        {
                            averageSalarystatus = "Below";
                            AgeColor = "#00ff00";
                        }
                        else
                        {
                            averageSalarystatus = "Same";
                            AgeColor = "#ffff00";
                        }
                        emp.Add(new employee
                        {
                            Id = item.Id,
                            employee_name = item.employee_name,
                            employee_age = item.employee_age,
                            employee_salary = item.employee_salary,
                            averageAgeSattus = Convert.ToString(averageagestatus),
                            averageSalaryStatus = averageSalarystatus,
                            statusColor = statusColor,
                            ageColor= AgeColor

                        });

                    }
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                   

                    review.employees = emp.ToPagedList(pageNumber,pageSize);
                }
                //eturn Json(weather, JsonRequestBehavior.AllowGet);
                //returning the employee list to view  


                return View(review);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}