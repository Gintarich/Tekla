using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimMakerLibrary.Models
{
    public enum ViewCreationType
    {
        Horizontal = 0,
        Vertical = 1,
        Both = 2,
    }
    public class CreationType
    {
        public string Name { get; }
        public ViewCreationType Type { get; }
        public double Location { get; }
        public int Count { get; }

        public CreationType(string name, ViewCreationType type, double location = 0, int count = 0)
        {
            Name = name;
            Type = type;
            Location = location;
            Count = count;
        }
    }
}
