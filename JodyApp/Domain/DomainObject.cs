﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public abstract class DomainObject : IEquatable<DomainObject>
    {
        [Key]
        public int? Id { get; set; } //can be null if new object

        bool IEquatable<DomainObject>.Equals(DomainObject other)
        {
            if (other == null) return false;
            return this.Id.Equals(other.Id);
        }

        public virtual bool AreTheSame(DomainObject obj) { return false; }

    }
}
