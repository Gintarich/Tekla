using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Filtering;
using DimAtr = Tekla.Structures.Drawing.StraightDimensionSet.StraightDimensionSetAttributes;

namespace DimmentionMaker.Models
{
    public static class AttributeProvider
    {
        public static DimAtr GetLeftMarkAttributes(Tekla.Structures.Drawing.ModelObject mo)
        {
            var attr = new DimAtr(mo);
            var font = attr.Text.Font;
            var udel = new UserDefinedElement("NAME");
            udel.Font = font;   
            var container = new ContainerElement{udel};
            attr.LeftUpperTag = container;
            attr.ExcludePartsAccordingToFilter = "0000_EXCLUDE_FILTER";
            return attr;
        }
        public static DimAtr GetRightMarkAttributes(Tekla.Structures.Drawing.ModelObject mo)
        {
            var attr = new DimAtr(mo);
            attr.ExcludePartsAccordingToFilter = "0000_EXCLUDE_FILTER";
            var font = attr.Text.Font;
            var udel = new UserDefinedElement("NAME");
            udel.Font = font;   
            var container = new ContainerElement{udel};
            attr.RightUpperTag = container;
            var test = StraightDimensionSet.GetAllExcludePartsAccordingToFilter();
            return attr;
        }
    }
}
