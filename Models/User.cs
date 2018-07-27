using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace beltexam.Models{
        public class User
    {
    
        public int UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="*first name is required")]
        public string firstname {get; set;}
        
        [Required(ErrorMessage="*last name is required")]
        [MinLength(2, ErrorMessage="*name must be at least 2 characters")]
        public string lastname{ get; set; }


        [Required(ErrorMessage="*username is required")]
        [MinLength(2,ErrorMessage="*alias must be at least 2 characters")]
        public string username{ get; set; }


        [Required(ErrorMessage="*email is required")]
        [EmailAddress(ErrorMessage = "*not a valid email address")]
        public string email{get; set;}


        [Required(ErrorMessage="*password is required")]
        [MinLength(8,ErrorMessage="*password must be at least 8 characters")]
        public string password { get; set; }
        [Compare("password",ErrorMessage="password must match")]

        public string confirm_password {get;set;}

        public int wallet {get; set;}

        public List<Bid> bids {get; set;}
        public List<Product> products {get; set;}

        public User()
        {
            bids = new List<Bid>();
            products = new List<Product>();
        }


    }





}
