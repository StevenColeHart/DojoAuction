using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace beltexam.Models{
        public class Product{

        public int ProductId { get; set; }

        public User creator{ get; set;}

        [Required(ErrorMessage= "name is required")]
        [MinLength(3, ErrorMessage="name must be at least 3 characters")]
        public string name {get; set;}

        [Required (ErrorMessage = "new post required")]
        [MinLength(10, ErrorMessage="Idea must be at least 10 characters")]
        public string description{ get; set;}

        [Required(ErrorMessage="auction end date required")]
        public DateTime? ending {get;set;}

        public DateTime? created_at {get; set;}

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage="starting bid must be at least $0")]
        public int startingbid {get; set;}

        public List<Bid> bid {get; set;}


        public Product(){
            bid = new List<Bid>();
        }
        
    }
}
