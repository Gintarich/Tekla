using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;

namespace ExtensionMethods
{
    public static class AssemblyExtensions
    {
        //This needs to take in subassembly name as well to be usefull
        public static PointList GetSubAssemblyPoints(this Assembly assembly)
        {
            PointList pointList = new PointList();
            var subAssemblies = assembly.GetSubAssemblies().Cast<Assembly>();
            foreach ( var subAssembly in subAssemblies)
            {
                Part p = subAssembly.GetMainPart() as Part;
                if (p is null) continue;
                pointList.Add(p.GetBox().GetCenterPoint());
            }
            return pointList;
        }

        public static PointList GetSubAssemblyPoints(this List<Assembly> assemblies)
        {
            PointList pointList = new PointList();
            foreach ( var assembly in assemblies)
            {
                Part p = assembly.GetMainPart() as Part;
                if(p is null) continue;
                pointList.Add(p.GetBox().GetCenterPoint() );
            }
            return pointList;
        }

        public static PointList GetPoints(this List<Assembly> assemblies,AABB box, Vector dir, string partName)
        {
            var subAssemblies = assemblies.Where(x => x.Name == partName).ToList();
            var points = subAssemblies.GetSubAssemblyPoints();
            var corners = new PointList()
            {
                box.MinPoint, // Bottom left
                new Point(box.MinPoint.X, box.MaxPoint.Y,0), // Top left
                box.MaxPoint, // Top right
                new Point(box.MaxPoint.X,box.MinPoint.Y,0) // Bottom right
            };
            points.AddRange(corners);
            if(dir == new Vector(1,0,0))
            {
                var rightBox = box.GetRight();
                var filteredPoints = points.FilterPoints(rightBox);
                return filteredPoints.RemoveRedundant(dir);
            }
            else if(dir == new Vector(-1, 0, 0))
            {
                var leftBox = box.GetLeft();
                var filteredPoints = points.FilterPoints(leftBox);
                return filteredPoints.RemoveRedundant(dir);
            }
            else if( dir == new Vector(0, 1,0))
            {
                var topBox = box.GetTop();
                var filteredPoints = points.FilterPoints(topBox);
                return filteredPoints.RemoveRedundant(dir);
            }
            else if(dir == new Vector(0,-1,0))
            {
                var bottomBox = box.GetBot();
                var filteredPoints = points.FilterPoints(bottomBox);
                return filteredPoints.RemoveRedundant(dir);
            }
            else { return null; }
        }
    }
}
