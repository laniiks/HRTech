using System;

namespace HRTech.Domain.Shared
{
    public abstract class MutableEntity<TId> : Entity<TId>
    {
        public DateTime? UpdateDateTime { get; set; }
    }
}