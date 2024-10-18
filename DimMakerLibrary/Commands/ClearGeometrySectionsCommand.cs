using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimMakerLibrary.Commands
{
    public class ClearGeometrySectionsCommand : IDrawingCommand
    {
        private readonly CastUnitDrawing _drawing;

        public ClearGeometrySectionsCommand(CastUnitDrawing drawing)
        {
            _drawing = drawing;
        }
        public void Execute(int idx)
        {
            var views = _drawing.GetSheet().GetAllViews().ToAList<View>().Where(x=>x.ViewType == View.ViewTypes.SectionView);
            if (!views.Any()) return;
            foreach (var view in views)
            {
                view.Delete();
            }
            _drawing.CommitChanges();
        }

        public CommandType GetCommandType()
        {
            return CommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 0;
        }
    }
}
