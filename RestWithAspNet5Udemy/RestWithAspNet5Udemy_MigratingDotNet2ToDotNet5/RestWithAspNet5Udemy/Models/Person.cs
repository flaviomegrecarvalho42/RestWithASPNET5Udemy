﻿using RestWithAspNet5Udemy.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet5Udemy.Models
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Gender")]
        public string Gender { get; set; }
    }
}
