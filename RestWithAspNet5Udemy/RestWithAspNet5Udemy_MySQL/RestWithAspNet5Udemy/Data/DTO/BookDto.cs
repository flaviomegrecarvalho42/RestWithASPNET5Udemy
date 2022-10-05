using RestWithAspNet5Udemy.Hypermedia;
using RestWithAspNet5Udemy.Hypermedia.Interfaces;
using System;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Data.DTO
{
    public class BookDto : ISupportHyperMedia
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}