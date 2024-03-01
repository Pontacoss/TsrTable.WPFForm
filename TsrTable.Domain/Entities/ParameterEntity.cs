﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.Domain.Entities
{
    public class ParameterEntity
    {
        private static int counter = 0;
        public int Id { get;  }
        public string Name { get; }
        public ParameterEntity(string name)
        {
            Id = counter;
            Name = name;
            counter++;
        }
    }
}
