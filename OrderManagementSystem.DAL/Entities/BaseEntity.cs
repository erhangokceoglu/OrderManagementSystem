namespace OrderManagementSystem.DAL.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid? CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
    public Guid? DeletedById { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
