using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoTklWebApplication.Models
{
    public class employee
    {
        public int Id { get; set; }
       
        public string employee_name { get; set; }
        public double employee_salary { get; set; }
        public int employee_age { get; set; }
        public string averageSalaryStatus { get; set; }

       public string averageAgeSattus { get; set; }
        public string statusColor { get; set; }
        public string ageColor { get; set; }
    }


}