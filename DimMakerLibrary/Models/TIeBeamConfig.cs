using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimMakerLibrary.Models
{
    public class TieBeamConfig
    {
        private static TieBeamConfig _instance = null;
        public static TieBeamConfig Instance => _instance ?? (_instance = new TieBeamConfig());
        public string GeoFrontViewName { get; set; } = "VIRSSKATS";
        public string ReinfFrontViewName { get; set; } = "STIEGROJUMS VIRSSKATS";
        public string ReinfSectionVerticalAttrName { get; set; } = "0000REINFVERTATTR.vi";
        public string GeoFrontViewAttrName { get; set; } = "0000GEOATTR.vi";
        public string ReinfFrontViewAttrName { get; set; } = "0000REINFATTR.vi";
        private TieBeamConfig() { }


    }
}
