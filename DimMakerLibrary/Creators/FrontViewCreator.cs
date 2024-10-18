using DimMakerLibrary.Commands;
using DimMakerLibrary.Interfaces;
using DimMakerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DimMakerLibrary.Creators
{
    public class FrontViewCreator : IViewCreator
    {
        private readonly ICommandQueue _commandQueue = new CommandQueue();
        private readonly Drawing _drawing;
        private TieBeamConfig _config = TieBeamConfig.Instance;

        public FrontViewCreator(Drawing drawing )
        {
            _drawing = drawing;
            SetupCommands();
        }

        private void SetupCommands()
        {
            Point GeoPt = CalculateGeoPoint();
            Point ReinfPt = CalculateReinfPoint();
            View.ViewAttributes geoAttributes = GetAttributes(_config.GeoFrontViewAttrName);
            View.ViewAttributes reinfAttributes = GetAttributes(_config.ReinfFrontViewAttrName);

            _commandQueue.Add(new ClearAllViewsCommand(_drawing));
            _commandQueue.Add(new CreateFrontViewCmd(GeoPt, _drawing, geoAttributes, _config.GeoFrontViewName));
            _commandQueue.Add(new CreateFrontViewCmd(ReinfPt, _drawing, reinfAttributes, _config.ReinfFrontViewName));
        }

        private View.ViewAttributes GetAttributes(string attr)
        {
            View.ViewAttributes attributes = new View.ViewAttributes();
            attributes.LoadAttributes(attr); 
            return attributes;
        }

        private Point CalculateGeoPoint()
        {
            // TODO: Implement logic
            return new Point(50,800,0);
        }

        private Point CalculateReinfPoint()
        {
            // TODO: Implement logic
            return new Point(50,500,0);
        }

        public void RunCommands()
        {
            _commandQueue.ExecuteCommands();
        }
    }
}
