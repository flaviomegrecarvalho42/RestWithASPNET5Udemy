using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Data.Mapper.Interfaces
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origins);
    }
}
