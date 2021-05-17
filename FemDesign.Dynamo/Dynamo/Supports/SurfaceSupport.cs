
using System.Xml.Serialization;

#region dynamo
using Autodesk.DesignScript.Runtime;
#endregion

namespace FemDesign.Supports
{
    [IsVisibleInDynamoLibrary(false)]
    public partial class SurfaceSupport: EntityBase
    {
        #region dynamo
        
        /// <summary>
        /// Create a SurfaceSupport element.
        /// </summary>
        /// <param name="surface">Surface defining the SurfaceSupport.</param>
        /// <param name="motions">"Motions release for the surface support.</param>
        /// <param name="localX">Set local x-axis. Vector must be perpendicular to surface local z-axis. Local y-axis will be adjusted accordingly. Optional, local x-axis from surface coordinate system used if undefined.</param>
        /// <param name="localZ">Set local z-axis. Vector must be perpendicular to surface local x-axis. Local y-axis will be adjusted accordingly. Optional, local z-axis from surface coordinate system used if undefined.</param>
        /// <param name="identifier">Identifier. Optional, default value if undefined.</param>
        [IsVisibleInDynamoLibrary(true)]
        public static SurfaceSupport SurfaceSupportDefine(Autodesk.DesignScript.Geometry.Surface surface, Releases.Motions motions, [DefaultArgument("Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)")] Autodesk.DesignScript.Geometry.Vector localX, [DefaultArgument("Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)")] Autodesk.DesignScript.Geometry.Vector localZ, [DefaultArgument("S")] string identifier)
        {
            // convert geometry
            Geometry.Region region = Geometry.Region.FromDynamo(surface);

            // create new surface support
            SurfaceSupport obj = new SurfaceSupport(region, motions, identifier);

            // set local x-axis
            if (!localX.Equals(Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)))
            {
                obj.CoordinateSystem.SetXAroundZ(FemDesign.Geometry.FdVector3d.FromDynamo(localX));
            }

            // set local z-axis
            if (!localZ.Equals(Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)))
            {
                obj.CoordinateSystem.SetZAroundX(FemDesign.Geometry.FdVector3d.FromDynamo(localZ));
            }

            // return
            return obj;
        }

        #endregion
    }
}