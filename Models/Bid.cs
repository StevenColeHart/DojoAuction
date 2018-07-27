using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace beltexam.Models
{
    public class Bid
    {
        public int BidId {get; set;}
        public User User {get;set;}
        public Product Product {get;set;}
        public int highestbid {get; set;}

        public Bid(){
        }

        public Bid(User User, Product Product){
            this.User  = User;
            this.Product = Product;
        }
        
    }
}