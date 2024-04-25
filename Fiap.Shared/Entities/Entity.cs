namespace Fiap.Shared.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
