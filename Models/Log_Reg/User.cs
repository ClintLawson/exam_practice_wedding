using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using exam_wedding_practice.Models;

namespace login_register.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}
        [Required]
        [MinLength(2, ErrorMessage="Must be 2 or more characters")]
        [Display(Name = "Last Name")]
        public string LastName{get;set;}

        [Required]
        [MinLength(2, ErrorMessage="Must be 2 or more characters")]
        [Display(Name = "First Name")]
        public string FirstName{get;set;}

        [Required]
        [EmailAddress]
        public string Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Must be 8 or more characters")]
        public string Password{get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        
        // [Required] //??? needed for initial validations??
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm{get;set;}

        //wedding relations
        public List<Wedding> Weddings{get;set;}
        public List<Guest> GuestAtWedding{get;set;}
    }
}