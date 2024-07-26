using DimmentionMaker.Commands;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using EdgeChamfer = Tekla.Structures.Model.EdgeChamfer;
using Part = Tekla.Structures.Model.Part;

namespace DimmentionMaker.Providers
{
    public class ChamferCmdProvider
    {
        private List<IDrawingCommand> _commands = new List<IDrawingCommand>();
        private readonly Assembly _assembly;
        private readonly View _view;

        public ChamferCmdProvider(Assembly assembly, View view)
        {
            _assembly = assembly;
            _view = view;
            CreateCommands();
        }

        public List<IDrawingCommand> GetCommands()
        {
            return _commands;
        }

        public void CreateCommands()
        {
            var mainPart = _assembly.GetMainPart() as Part;
            if (mainPart is null) { return; }
            var chamfers = mainPart.GetBooleans().ToList().Where(x => x is EdgeChamfer).Cast<EdgeChamfer>().ToList();
            //Filter out paralele chamfers
            chamfers = chamfers.Where(x => x.IsParallel(new Vector(0, 0, 1))).ToList();
            //Foreach chamfer check if its top or bot chamfer
            foreach (var chamfer in chamfers)
            {
                var ptSum = (chamfer.SecondEnd + chamfer.FirstEnd);
                var midPt = new Point(ptSum.X / 2, ptSum.Y / 2, ptSum.Z / 2);
                var viewBox = _view.RestrictionBox;
                var box = _assembly.GetBox();
                var botBox = box.GetBot();
                var topBox = box.GetTop();
                var scale = _view.Attributes.Scale;
                if (botBox.IsInside(midPt) && viewBox.GetOBB().Intersects(new LineSegment(chamfer.FirstEnd,chamfer.SecondEnd)) )
                {
                    midPt.Y -= 2 * scale;
                    _commands.Add(new AddChamferMarkCommand(_view, midPt));
                }
                if (topBox.IsInside(midPt) && viewBox.GetOBB().Intersects(new LineSegment(chamfer.FirstEnd,chamfer.SecondEnd)))
                {
                    midPt.Y += 2 * scale;
                    _commands.Add(new AddChamferMarkCommand(_view, midPt));
                }
            }
        }
    }
}
