using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;
using Tekla.Structures.Model;

namespace ExtensionMethods
{
    public static class TeklaExtensionMethods
    {
		public static List<Part> GetAllParts(this Model model, bool autoFetch)
        {
            //IMPORTANT!!!
            ModelObjectEnumerator.AutoFetch = autoFetch;

            //parts
            var types = new[] { typeof(Beam), typeof(BentPlate),
                typeof(ContourPlate), typeof(PolyBeam) };

            var parts = model
                .GetModelObjectSelector()
                .GetAllObjectsWithType(types)
                .ToAList<Part>();

            return parts;
        }
        public static List<T> ToAList<T>(this IEnumerator enumerator)
        {
            var list = new List<T>();
            while (enumerator.MoveNext())
            {
                var current = (T)enumerator.Current;
                if (current != null)
                    list.Add(current);
            }
            return list;
        }

        //Filter properties --------------------------------------------------
        public static List<Part> GetParts(this Model model, bool autoFetch)
        {
            ObjectFilterExpressions.Type objectType = new ObjectFilterExpressions.Type();
            NumericConstantFilterExpression type =
                new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.PART);

            var expression2 = new BinaryFilterExpression(objectType, NumericOperatorType.IS_EQUAL, type);

            BinaryFilterExpressionCollection filterCollection =
                new BinaryFilterExpressionCollection
                {
                    new BinaryFilterExpressionItem(expression2, BinaryFilterOperatorType.BOOLEAN_AND),
                };

            //IMPORTANT!!!
            ModelObjectEnumerator.AutoFetch = autoFetch;

            return model
                .GetModelObjectSelector()
                .GetObjectsByFilter(filterCollection)
                .ToAList<Part>();
        }
        public static List<Assembly> GetAllAssemblies(this Model model, bool autoFetch)
        {
            //IMPORTANT!!!
            ModelObjectEnumerator.AutoFetch = autoFetch;

            var assemblies = model
                .GetModelObjectSelector()
                .GetAllObjectsWithType(ModelObject.ModelObjectEnum.ASSEMBLY)
                .ToAList<Assembly>();

            return assemblies;
        }
        public static List<Assembly> GetAssembies(this Model model, bool autoFetch)
        {
            ObjectFilterExpressions.Type objectType = new ObjectFilterExpressions.Type();
            NumericConstantFilterExpression type =
                new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.ASSEMBLY);

            TemplateFilterExpressions.CustomString mainpartMaterial = new TemplateFilterExpressions.CustomString("MAINPART.MATERIAL_TYPE");

            TemplateFilterExpressions.CustomString assemblyLevel = new TemplateFilterExpressions.CustomString("HIERARCHY_LEVEL");
            StringConstantFilterExpression level = new StringConstantFilterExpression("0");

            var expression2 = new BinaryFilterExpression(objectType, NumericOperatorType.IS_EQUAL, type);
            var expression3 = new BinaryFilterExpression(mainpartMaterial, StringOperatorType.IS_EQUAL,
                new StringConstantFilterExpression("STEEL"));
            var expression4 = new BinaryFilterExpression(assemblyLevel, StringOperatorType.IS_EQUAL,level);


            BinaryFilterExpressionCollection filterCollection =
                new BinaryFilterExpressionCollection
                {
                    new BinaryFilterExpressionItem(expression2, BinaryFilterOperatorType.BOOLEAN_AND),
                    new BinaryFilterExpressionItem(expression3, BinaryFilterOperatorType.BOOLEAN_AND),
                    new BinaryFilterExpressionItem(expression4, BinaryFilterOperatorType.BOOLEAN_AND)
                };

            //IMPORTANT!!!
            ModelObjectEnumerator.AutoFetch = autoFetch;

            return model
                .GetModelObjectSelector()
                .GetObjectsByFilter(filterCollection)
                .ToAList<Assembly>()
                ;
        }
        public static ModelObjectEnumerator GetAssembieNummerator(this Model model, bool autoFetch)
        {
            ObjectFilterExpressions.Type objectType = new ObjectFilterExpressions.Type();
            NumericConstantFilterExpression type =
                new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.ASSEMBLY);

            TemplateFilterExpressions.CustomString mainpartMaterial = new TemplateFilterExpressions.CustomString("MAINPART.MATERIAL_TYPE");

            TemplateFilterExpressions.CustomString assemblyLevel = new TemplateFilterExpressions.CustomString("HIERARCHY_LEVEL");
            StringConstantFilterExpression level = new StringConstantFilterExpression("0");

            var expression2 = new BinaryFilterExpression(objectType, NumericOperatorType.IS_EQUAL, type);
            var expression3 = new BinaryFilterExpression(mainpartMaterial, StringOperatorType.IS_EQUAL,
                new StringConstantFilterExpression("STEEL"));
            var expression4 = new BinaryFilterExpression(assemblyLevel, StringOperatorType.IS_EQUAL, level);


            BinaryFilterExpressionCollection filterCollection =
                new BinaryFilterExpressionCollection
                {
                    new BinaryFilterExpressionItem(expression2, BinaryFilterOperatorType.BOOLEAN_AND),
                    new BinaryFilterExpressionItem(expression3, BinaryFilterOperatorType.BOOLEAN_AND),
                    new BinaryFilterExpressionItem(expression4, BinaryFilterOperatorType.BOOLEAN_AND)
                };

            //IMPORTANT!!!
            ModelObjectEnumerator.AutoFetch = autoFetch;

            return model
                    .GetModelObjectSelector()
                    .GetObjectsByFilter(filterCollection)
                ;
        }

        
        public static string GetStringReportProperty(this Assembly assembly, string reportProperty)
        {
            string repProp = string.Empty;
            assembly.GetReportProperty(reportProperty, ref repProp);

            return repProp;
        }
        public static int GetIntegerReportProperty(this Assembly assembly, string reportProperty)
        {
            int repProp = 0;
            assembly.GetReportProperty(reportProperty, ref repProp);

            return repProp;
        }
        public static double GetDoubleReportProperty(this Assembly assembly, string reportProperty)
        {
            double repProp = 0;
            assembly.GetReportProperty(reportProperty, ref repProp);

            return repProp;
        }


        public static List<ModelObject> ToList(this ModelObjectEnumerator enumerator)
        {
            var modelObjects = new List<ModelObject>();
            while (enumerator.MoveNext())
            {
                var modelObject = enumerator.Current;
                modelObjects.Add(modelObject);
            }
            return modelObjects;
        }
        public static void SelectModelObjectsInUi(List<int> ids)
        {
            Model model = new Model();
            var modelObjects = new ArrayList();

            ids.ForEach(id =>
            {
                var modelObject = model.SelectModelObject(new Identifier(id));
                if (modelObject == null) return;
                modelObjects.Add(modelObject);
            });

            var selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            selector.Select(modelObjects);
        }

    }
}
