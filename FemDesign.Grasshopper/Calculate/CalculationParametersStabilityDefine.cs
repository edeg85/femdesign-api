﻿// https://strusoft.com/
using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;

namespace FemDesign.Grasshopper
{
    public class CalculationParametersStabilityDefine : FEM_Design_API_Component
    {
        public CalculationParametersStabilityDefine() : base("Stability.Define", "Stability", "Set parameters for stability.", CategoryName.Name(), SubCategoryName.Cat7a())
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("LoadCombination", "LoadCombination", "Load combinations or load combination names for which the stability analysis will be run.", GH_ParamAccess.list);
            pManager.AddIntegerParameter("NumShapes", "NumShapes", "Number of shapes to calculate.", GH_ParamAccess.list);
            pManager[pManager.ParamCount - 1].Optional = true;
            pManager.AddBooleanParameter("PositiveOnly", "PositiveOnly", "Not yet implemented.", GH_ParamAccess.item, false);
            pManager[pManager.ParamCount - 1].Optional = true;
            pManager.AddIntegerParameter("NumIterations", "NumIterations", "Not yet implemented.", GH_ParamAccess.item, 20);
            pManager[pManager.ParamCount - 1].Optional = true;
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Stability", "Stability", "Stability.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var _loadCombination = new List<dynamic>();
            if(!DA.GetDataList(0, _loadCombination)) return;

            List<string> loadCombination = new List<string>();
            foreach (dynamic obj in _loadCombination)
            {
                if (obj.Value is string str)
                {
                    loadCombination.Add(str);
                }
                else if(obj.Value is FemDesign.Loads.LoadCombination loads)
                {
                    loadCombination.Add(loads.Name);
                }
            }


            var numShapes = new List<int>();
            if( !DA.GetDataList(1, numShapes))
            {
                numShapes = Enumerable.Repeat(1, loadCombination.Count).ToList();
            };

            bool positiveOnly = false;
            DA.GetData(2, ref positiveOnly);

            int numIterations = 20;
            DA.GetData(3, ref numIterations); //uncomment the line when we get response from the developer.

            var _obj = new FemDesign.Calculate.Stability(loadCombination, numShapes, positiveOnly, numIterations);

            DA.SetData(0, _obj);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return FemDesign.Properties.Resources.StabilityDefine;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{8EC22396-ECC6-4E8E-A23D-2FFC1F0BC3F9}"); }
        }
        public override GH_Exposure Exposure => GH_Exposure.tertiary;

    }
}