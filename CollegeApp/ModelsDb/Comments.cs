using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.ModelsDb
{
    public class Comments
    {
        public Comments()
        {
        }
        public int CommentID { get; set; }
        public Nullable<int> CollegeID { get; set; }
        public string CommentDescription { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}