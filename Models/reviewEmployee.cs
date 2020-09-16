using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoTklWebApplication.Models
{
    public class reviewEmployee
    {
       

        public double averageAge { get; set; }
        public double averageSalary { get; set; }
        public IPagedList<employee> employees { get; set; }
    }
}