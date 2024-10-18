using DimMakerLibrary.Models;
using Tekla.Structures.Geometry3d;

namespace DimMakerLibrary
{
    public static class Utils
    {
        public static CommandType GetDimCommandTypeFromDir(Vector dir)
        {
            if (dir == Dirrections.Left)
            {
                return CommandType.LeftDimmension;
            }
            else if (dir == Dirrections.Right)
            {
                return CommandType.RightDimmension;
            }
            else if (dir == Dirrections.Top)
            {
                return CommandType.TopDimmension;
            }
            else if (dir == Dirrections.Bottom)
            {
                return CommandType.BottomDimmension;
            }
            else
            {
                return CommandType.NotADimmension;
            }
        }
        public static Vector GetVector(this CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.TopDimmension: return Dirrections.Top;
                case CommandType.BottomDimmension: return Dirrections.Bottom;
                case CommandType.LeftDimmension: return Dirrections.Left;
                case CommandType.RightDimmension: return Dirrections.Right;
                default: return null;
            }
        }
    }
}
