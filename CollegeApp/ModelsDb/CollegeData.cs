using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;

namespace CollegeApp.ModelsDb
{
    public class CollegeData
    {
        internal List<Comment>college;
        public CollegeData()
        {
            comments = new List<Comments>();
        }
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public List<Comments> comments { get; set; }
    }
}