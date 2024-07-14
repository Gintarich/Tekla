using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimmentionMaker
{
    enum SectionType
    {
        Geometry = 0,
        Reinforcement = 1,
        Unknown = 2,
    }

    static class GeoSectionTypeManager
    {
        static readonly string _propName = "SectionType";
        static readonly string _type = "GeometrySection";

        static public void SetSectionType(View view)
        {
            view.SetUserProperty(_propName, _type);
            view.Modify();
        }
        static public SectionType GetSectionType(View view)
        {
            string s = "";
            view.GetUserProperty(_propName, ref s);
            if (s == _type) return SectionType.Geometry;
            else return SectionType.Unknown;
        }
    }
}
