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
        private static readonly string _propName = "SectionType";
        private static readonly Dictionary<SectionType, string> _map = new Dictionary<SectionType, string>()
        {
            {SectionType.Geometry, "GeometrySection" },
            {SectionType.Reinforcement, "ReinforcementSection" },
            {SectionType.Unknown, "Unknown" },
        };

        static public void SetSectionType(View view, SectionType type)
        {
            view.SetUserProperty(_propName, _map[type]);
            view.Modify();
        }
        static public SectionType GetSectionType(View view)
        {
            string s = "";
            view.GetUserProperty(_propName, ref s);
            return _map.FirstOrDefault(x => x.Value == s).Key;
        }
    }
}
