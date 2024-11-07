using System.ComponentModel.DataAnnotations;

namespace Customer.BusinessLogic.Database.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
