using DimmentionMaker.Models;
using Tekla.Structures.Geometry3d;

namespace DimmentionMaker
{
    public static class Utils
    {
        public static DimmensionCommandType GetDimCommandTypeFromDir(Vector dir)
        {
            if (dir == Dirrections.Left)
            {
                return DimmensionCommandType.LeftDimmension;
            }
            else if (dir == Dirrections.Right)
            {
                return DimmensionCommandType.RightDimmension;
            }
            else if (dir == Dirrections.Top)
            {
                return DimmensionCommandType.TopDimmension;
            }
            else if (dir == Dirrections.Bottom)
            {
                return DimmensionCommandType.BottomDimmension;
            }
            else
            {
                return DimmensionCommandType.NotADimmension;
            }
        }
        public static Vector GetVector(this DimmensionCommandType commandType)
        {
            switch (commandType)
            {
                case DimmensionCommandType.TopDimmension: return Dirrections.Top;
                case DimmensionCommandType.BottomDimmension: return Dirrections.Bottom;
                case DimmensionCommandType.LeftDimmension: return Dirrections.Left;
                case DimmensionCommandType.RightDimmension: return Dirrections.Right;
                default: return null;
            }
        }
    }
}
