using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSDatatype = Tekla.Structures.Datatype;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TSPlugins = Tekla.Structures.Plugins;

namespace Rezgogi
{
    public class StructuresData
    {
        //   [TSPlugins.StructuresField("ATTRIBUTE_NAME")]
        //   public VARIABLE_TYPE VARIABLE_NAME;
        [TSPlugins.StructuresField("Garums")]
        public double Garums;

        [TSPlugins.StructuresField("Platums")]
        public double Platums;
    }
    [TSPlugins.Plugin("Rezgogi")]
    [TSPlugins.PluginUserInterface("Rezgogi.MainForm")]
    public class MainPlugin : TSPlugins.PluginBase
    {
        // Enable retrieving of input values
        private readonly StructuresData _data;

        private double _garums;
        private double _platums;

        // Enable inserting of objects in a model
        private readonly Model _model;
        public Model Model
        {
            get { return _model; }
        }
        
        public MainPlugin(StructuresData data)
        {
            // Link to model.         
            _model = new Model();
            // Link to input values.         
            _data = data;
        }

        // Specify the user input needed for the plugin.
        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> inputList = new List<InputDefinition>();
            Picker picker = new Picker();

            Beam beam = (Beam)picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART, "Izvelies kolonnu");

            var input = new InputDefinition(beam.Identifier);

            inputList.Add(input);

            return inputList;
        }

        // This method is called upon execution of the plug-in and it´s the main method of the plug-in
        public override bool Run(List<InputDefinition> input)
        {
            try
            {
                GetValuesFromDialog();
                MessageBox.Show(_garums.ToString(), _platums.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return true;
        }
        //User methods

        private void GetValuesFromDialog()
        {
            _garums = _data.Garums;
            _platums = _data.Platums;

            if (IsDefaultValue(_garums))
            {
                _garums = 1000.0;
            }
            if (IsDefaultValue(_platums))
            {
                _platums = 1000.0;
            }
        }
    }
}
