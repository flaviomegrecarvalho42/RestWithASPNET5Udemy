using RestWithAspNet5Udemy.Hypermedia;
using RestWithAspNet5Udemy.Hypermedia.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Data.DTO
{
    public class PersonDto : ISupportHyperMedia
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}