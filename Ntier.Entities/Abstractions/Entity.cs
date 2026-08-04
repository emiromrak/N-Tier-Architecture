namespace NTier.Entities.Abstractions;
    public abstract class Entity
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }