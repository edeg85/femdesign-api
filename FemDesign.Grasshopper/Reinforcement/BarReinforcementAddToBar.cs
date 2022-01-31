// https://strusoft.com/
using System;
using System.Collections.Generic;
using Grasshopper.Kernel;

namespace FemDesign.Grasshopper
{
    public class BarReinforcementAddToBar: GH_Component
    {
        public BarReinforcementAddToBar(): base("BarReinforcement.AddToBar", "AddToBar", "Add bar reinforcement to bar.", "FemDesign", "Reinforcement")
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bar", "Bar", "Bar.", GH_ParamAccess.item);
            pManager.AddGenericParameter("BarReinforcement", "BarReinforcement", "BarReinforcment to add to bar. Item or list.", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Overwrite", "Overwrite", "Overwrite if reinforcement bar already exists on bar - by guid.", GH_ParamAccess.item, true);
            pManager[pManager.ParamCount - 1 ].Optional = true;
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bar", "Bar", "Passed bar.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // get data
            FemDesign.Bars.Bar bar = null;
            if (!DA.GetData(0, ref bar))
            {
                return;
            }

            List<FemDesign.Reinforcement.BarReinforcement> barReinforcement = new List<FemDesign.Reinforcement.BarReinforcement>();
            if (!DA.GetDataList(1, barReinforcement))
            {
                return;
            }

            bool overwrite = true;
            if (!DA.GetData(2, ref overwrite))
            {
                return;
            }

            if (bar == null || barReinforcement == null)
            {
                return;
            }

            // clone bar
            var clone = bar.DeepClone();

            // add reinforcement
            FemDesign.Bars.Bar obj = FemDesign.Reinforcement.BarReinforcement.AddReinforcementToBar(clone, barReinforcement, overwrite);

            // return
            DA.SetData(0, obj);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("68bcde26-f12e-446b-8651-f7b93adee726"); }
        }
    }   
}