﻿using System;
using System.Collections.Generic;
using Grasshopper;
using Grasshopper.Kernel;
using System.Linq;
using Rhino.Geometry;
using FemDesign.Results;

namespace FemDesign.Grasshopper
{
    public class FeaNode : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FeaNode class.
        /// </summary>
        public FeaNode()
          : base("Results.FeaNode", "FeaNode",
              "Deconstruct an Fea Node in his Part",
              "FEM-Design", "Results")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("FeaNode", "FeaNode", "Result to be Parse", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_StringParam("NodeId", "NodeId", "Node Index");
            pManager.Register_PointParam("Position", "Pos", "Node Geometry [mm]");
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var FeaNode = new List<FemDesign.Results.FeaNode>();
            DA.GetDataList("FeaNode", FeaNode);

            // Read Result from Abstract Method
            var result = FemDesign.Results.FeaNode.DeconstructFeaNode(FeaNode);


            var nodeId = (List<int>) result["NodeId"];
            var feaNodePoint = (List<FemDesign.Geometry.FdPoint3d>) result["Position"];


            // Convert the FdPoint to Rhino
            var ofeaNodePoint = feaNodePoint.Select(x => x.ToRhino());

            // Set output
            DA.SetDataList("NodeId", nodeId);
            DA.SetDataList("Position", ofeaNodePoint);
        }

        public override GH_Exposure Exposure => GH_Exposure.secondary;

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("E014AD4B-F88D-4D5A-B31B-9558BACB4C9F"); }
        }
    }
}