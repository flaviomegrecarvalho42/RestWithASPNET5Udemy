using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet5Udemy.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
