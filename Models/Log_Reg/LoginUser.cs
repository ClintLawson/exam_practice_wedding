using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_register.Models
{
    public class LoginUser
    {

        //This class is necessary because using the User class for log in would throw and error for unpopulated properties
        //this is a classic example of a viewModel, its a model only for a view, not a database
        [Required]
        public string Email{get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password{get;set;}
    }
}