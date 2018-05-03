using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoShare.Models
{
    public class UserModels
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}