using RestWithAspNet5Udemy.Hypermedia.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
