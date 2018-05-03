using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoShare.Models
{
    public class RoomModels
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public string RoomUrl { get; set; }
        public string RoomOwner { get; set; }
    }
}