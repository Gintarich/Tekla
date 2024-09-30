using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationModel
{
    public class SteelElement :BaseElement
    {
        public SteelElement(string position,
            string name, 
            int count, 
            string profile, 
            double length,
            double weight,
            double area,
            double totalWeight,
            double totalArea)
        {
            Position = position;
            Name = name;
            Count = count;
            Profile = profile;
            Length = length;
            Weight = weight;
            Area = area;
            TotalWeight = totalWeight;
            TotalArea = totalArea;
        }

        public string Position { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Profile { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public double TotalWeight { get; set; }
        public double TotalArea { get; set; }

    }
}
