﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ReferenceObject
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public ReferenceObject() { }
        public ReferenceObject(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
