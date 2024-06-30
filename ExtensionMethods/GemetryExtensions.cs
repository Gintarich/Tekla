using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.TeklaStructuresInternal.MateriaClient;
using Part = Tekla.Structures.Model.Part;

namespace ExtensionMethods
{
    public static class GemetryExtensions
    {
        public static PointList FilterPoints(this PointList list, AABB box)
        {
            AABB top = box.GetTop();
            PointList pList = new PointList();
            foreach (Point pt in list)
            {
                if (box.IsInside(pt)) pList.Add(pt);
            }
            return pList;
        }
        public static PointList RemoveRedundant(this PointList list, Vector dir)
        {
            if (list.Count <= 1) return list;
            PointList pList = new PointList();
            if (dir == new Vector(0, -1, 0) || dir == new Vector(0, 1, 0))
            {
                list.Sort((p1, p2) => p1.X > p2.X);
                pList.Add(list[0]);
                for (int i = 1; i < list.Count; i++)
                {
                    if (!(list[i].X == list[i - 1].X)) pList.Add(list[i]);
                }
            }
            else
            {
                list.Sort((p1, p2) => p1.Y > p2.Y);
                pList.Add(list[0]);
                for (int i = 1; i < list.Count; i++)
                {
                    if (!(list[i].Y == list[i - 1].Y)) pList.Add(list[i]);
                }
            }
            return pList;
        }

        #region SortingPointList
        private static void swap(this PointList list, int i, int j)
        {
            Point temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        private static int Partition(this PointList list, Func<Point, Point, bool> lessThan, int l, int r)
        {
            int index = l;
            Point pivot = list[l];
            for (int i = l + 1; i <= r; i++)
            {
                if (lessThan(pivot, list[i]))
                {
                    index++;
                    swap(list, i, index);
                }
            }
            swap(list, l, index);
            return index;
        }
        public static void Sort(this PointList list, Func<Point, Point, bool> lessThan)
        {
            var left = 0;
            var right = list.Count - 1;
            if (right > left)
            {
                var pivot = Partition(list, lessThan, left, right);
                Sort(list, lessThan, left, pivot - 1);
                Sort(list, lessThan, pivot + 1, right);
            }
        }
        public static void Sort(this PointList list, Func<Point, Point, bool> lessThan, int left, int right)
        {
            if (right > left)
            {
                var pivot = Partition(list, lessThan, left, right);
                Sort(list, lessThan, left, pivot - 1);
                Sort(list, lessThan, pivot + 1, right);
            }
        }
        #endregion

        public static AABB GetTop(this AABB box)
        {
            var halfHeight = (box.MaxPoint.Y + box.MinPoint.Y) / 2;
            var minPt = new Point(box.MinPoint.X, halfHeight, box.MinPoint.Z);
            return new AABB(minPt, box.MaxPoint);
        }
        public static AABB GetBot(this AABB box)
        {
            var halfHeight = (box.MaxPoint.Y + box.MinPoint.Y) / 2;
            var maxPt = new Point(box.MaxPoint.X, halfHeight, box.MaxPoint.Z);
            return new AABB(box.MinPoint, maxPt);
        }
        public static AABB GetLeft(this AABB box)
        {
            var halfLength = (box.MaxPoint.X + box.MinPoint.X) / 2;
            var maxPt = new Point(halfLength, box.MaxPoint.Y, box.MaxPoint.Z);
            return new AABB(box.MinPoint, maxPt);
        }
        public static AABB GetRight(this AABB box)
        {
            var halfLength = (box.MaxPoint.X + box.MinPoint.X) / 2;
            var minPt = new Point(halfLength, box.MinPoint.Y, box.MinPoint.Z);
            return new AABB(minPt, box.MaxPoint);
        }
        public static AABB GetBox(this Part part)
        {
            var solid = part.GetSolid();
            return new AABB(solid.MinimumPoint, solid.MaximumPoint);
        }
        public static AABB GetBox(this Assembly assembly)
        {
            Part p = assembly.GetMainPart() as Part;
            if (p == null) return null;
            var solid = p.GetSolid();
            return new AABB(solid.MinimumPoint, solid.MaximumPoint);
        }
    }
}
