// https://strusoft.com/

using System.Xml.Serialization;

#region dynamo
using Autodesk.DesignScript.Runtime;
#endregion

namespace FemDesign.Supports
{
    [System.Serializable]
    [IsVisibleInDynamoLibrary(false)]
    public class SurfaceSupport: EntityBase
    {
        [XmlAttribute("name")]
        public string _name;
        [XmlIgnore]
        public string Identifier
        {
            get
            {
                return this._name;
            }
            set
            {
                PointSupport.instance++;
                this._name = value + "." + PointSupport.instance.ToString();
            }
        }
        [XmlElement("region", Order=1)]
        public Geometry.Region Region { get; set; }
        [XmlElement("rigidity", Order=2)]
        public Releases.RigidityDataType1 Rigidity { get; set; }
        [XmlElement("local_system", Order=3)]
        public Geometry.FdCoordinateSystem LocalSystem { get; set; }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        private SurfaceSupport()
        {

        }

        /// <summary>
        /// Internal constructor
        /// </summary>
        public SurfaceSupport(Geometry.Region region, Releases.RigidityDataType1 rigidity)
        {
            this.EntityCreated();
            this.Identifier = "S";
            this.Region = region;
            this.Rigidity = rigidity;
            this.LocalSystem = region.coordinateSystem;
        }

        /// <summary>
        /// Internal constructor with only translation rigidity defined
        /// </summary>
        public SurfaceSupport(Geometry.Region region, Releases.Motions motions)
        {
            this.EntityCreated();
            this.Identifier = "S";
            this.Region = region;
            this.Rigidity = new Releases.RigidityDataType1(motions);
            this.LocalSystem = region.coordinateSystem;
        }

        #region dynamo
        /// <summary>
        /// Create a SurfaceSupport element.
        /// </summary>
        /// <param name="surface">Surface defining the SurfaceSupport.</param>
        /// <param name="motions">"Motions release for the surface support.</param>
        /// <param name="localX">Set local x-axis. Vector must be perpendicular to surface local z-axis. Local y-axis will be adjusted accordingly. Optional, local x-axis from surface coordinate system used if undefined.</param>
        /// <param name="localZ">Set local z-axis. Vector must be perpendicular to surface local x-axis. Local y-axis will be adjusted accordingly. Optional, local z-axis from surface coordinate system used if undefined.</param>
        [IsVisibleInDynamoLibrary(true)]
        public static SurfaceSupport SurfaceSupportDefine(Autodesk.DesignScript.Geometry.Surface surface, Releases.Motions motions, [DefaultArgument("Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)")] Autodesk.DesignScript.Geometry.Vector localX, [DefaultArgument("Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)")] Autodesk.DesignScript.Geometry.Vector localZ)
        {
            // convert geometry
            Geometry.Region region = Geometry.Region.FromDynamo(surface);

            // create new surface support
            SurfaceSupport obj = new SurfaceSupport(region, motions);

            // set local x-axis
            if (!localX.Equals(Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)))
            {
                obj.LocalSystem.localX = FemDesign.Geometry.FdVector3d.FromDynamo(localX);
            }

            // set local z-axis
            if (!localZ.Equals(Autodesk.DesignScript.Geometry.Vector.ByCoordinates(0,0,0)))
            {
                obj.LocalSystem.localZ = FemDesign.Geometry.FdVector3d.FromDynamo(localZ);
            }

            // return
            return obj;
        }
        #endregion
    }
}