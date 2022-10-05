using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Hypermedia.Interfaces
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
