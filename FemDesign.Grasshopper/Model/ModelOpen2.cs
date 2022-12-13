// https://strusoft.com/
using System;
using Grasshopper.Kernel;

namespace FemDesign.Grasshopper
{
    public class ModelOpen2 : GH_Component
    {
        public ModelOpen2() : base("Model.Open", "Open", "Open model in FEM-Design.", CategoryName.Name(), SubCategoryName.Cat6())
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Connection", "Connection", "FEM-Design connection.", GH_ParamAccess.item);
            pManager.AddGenericParameter("FdModel", "FdModel", "FdModel to open.", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RunNode", "RunNode", "If true node will execute. If false node will not execute.", GH_ParamAccess.item, true);
            pManager[pManager.ParamCount - 1].Optional = true;
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Connection", "Connection", "FEM-Design connection.", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Success", "Success", "", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            FemDesignConnection connection = null;
            Model model = null;
            bool runNode = false;
            if (!DA.GetData("Connection", ref connection)) return;
            if (!DA.GetData("FdModel", ref model)) return;
            DA.GetData("RunNode", ref runNode);


            bool success = false;
            if (runNode)
            {
                connection.Open(model);
                while (connection.IsRunning())
                {
                    System.Threading.Thread.Sleep(100);
                }

                success = true;
            }
            else
            {
                success = false;
            }

            DA.SetData("Connection", connection);
            DA.SetData("Success", success);
        }
        protected override System.Drawing.Bitmap Icon => base.Icon;
        public override Guid ComponentGuid => new Guid("96dc72e0-c0c1-4081-ac2b-56be85905fb2");
        public override GH_Exposure Exposure => GH_Exposure.secondary;
    }
}