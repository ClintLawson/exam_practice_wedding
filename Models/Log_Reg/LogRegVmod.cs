using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_register.Models
{
    public class LogRegVmod
    {
        public User register{get;set;}
        public LoginUser login{get;set;}
    }
}