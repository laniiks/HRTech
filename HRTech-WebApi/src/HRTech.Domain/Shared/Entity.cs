using System;

namespace HRTech.Domain.Shared
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}