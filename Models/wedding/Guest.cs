using System;
using System.ComponentModel.DataAnnotations;
using login_register.Models;

namespace exam_wedding_practice.Models
{
    public class Guest
    {
        [Key]
        public int GuestId{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int WeddingId{get;set;}
        public Wedding Wedding{get;set;}
    }
}