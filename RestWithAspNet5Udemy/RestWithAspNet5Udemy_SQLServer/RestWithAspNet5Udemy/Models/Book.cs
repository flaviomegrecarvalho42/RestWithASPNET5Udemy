﻿using RestWithAspNet5Udemy.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet5Udemy.Models
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
    }
}