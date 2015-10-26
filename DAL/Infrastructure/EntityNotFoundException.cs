namespace DAL.Infrastructure
{
    using System;

    public class EntityNotFoundException : Exception
    {
        private string entityId;

        public EntityNotFoundException(string message, string entityId) : this(message)
        {
            this.entityId = entityId;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}