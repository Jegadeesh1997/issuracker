using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace isuuetracker.Models
{
    public class Modelclass
    {
        [Required]
        [StringLength(20,MinimumLength =2)]
        public string username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string password { get; set; }
        public string bugname { get; set; }
        public string bugtype { get; set; }
        public string comments { get; set; }
        public string projid { get; set; }
        public int bugid { get; set; }
        public string status { get; set; }
        public int devid { get; set; }
        public DateTime time { get; set; }
        public string job { get; set; }
        public List<SelectListItem> projectlist = new List<SelectListItem>();
        public List<SelectListItem> dev = new List<SelectListItem>();
    }
}