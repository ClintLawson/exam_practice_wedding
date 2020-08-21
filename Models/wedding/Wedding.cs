using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using login_register.Models;

namespace exam_wedding_practice.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId{get;set;}
        [Required]
        public int OwnerId{get;set;}
        public User Owner{get;set;}
        [Required]
        [Display(Name="Wedder One")]
        public string Wedder1{get;set;}
        [Required]
        [Display(Name="Wedder Two")]
        public string Wedder2{get;set;}
        [Required]
        [IsFutureDate]
        [DataType(DataType.Date)]
        public DateTime Date{get;set;}
        [Required]
        [Display(Name="Wedding Address")]
        public string Location{get;set;}
        public List<Guest> GuestList{get;set;}

        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
    }
}