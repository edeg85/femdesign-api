// https://strusoft.com/
using System;
using System.Xml.Serialization;

#region dynamo
using Autodesk.DesignScript.Runtime;
#endregion

namespace FemDesign.Geometry
{
    [System.Serializable]
    [IsVisibleInDynamoLibrary(false)]
    public class FdCoordinateSystem
    {
        [XmlElement("local_pos", Order=1)]
        public FdPoint3d origin { get; set; }
        [XmlElement("local_x", Order=2)]
        public FdVector3d _localX;
        [XmlIgnore]
        public FdVector3d localX
        {
            get
            {
                return this._localX;
            }
            set
            {
                Geometry.FdVector3d val = value.Normalize();
                Geometry.FdVector3d z = this.localZ;

                double dot = z.Dot(val);
                if (Math.Abs(dot) < Tolerance.dotProduct)
                {
                    this._localX = val;
                    this._localY = z.Cross(val); // follows right-hand-rule
                }
                
                else
                {
                    throw new System.ArgumentException($"X-axis is not perpendicular to Z-axis. The dot-product is {dot}, but should be 0");
                }
            }
        }        
        [XmlElement("local_y", Order=3)]
        public FdVector3d _localY;
        [XmlIgnore]
        public FdVector3d localY
        {
            get
            {
                return this._localY;
            }
        }
        [XmlIgnore]
        public FdVector3d _localZ;
        [XmlIgnore]
        public FdVector3d localZ
        {
            get
            {
                if (this.localX == null || this.localY == null)
                {
                    throw new System.ArgumentException("Impossible to construct z-axis as either this.localX or this.localY is null.");
                }
                else
                {     
                    return this.localX.Cross(localY).Normalize();
                }
            }
            set
            {
                Geometry.FdVector3d val = value.Normalize();
                Geometry.FdVector3d x = this.localX;

                double dot = x.Dot(val);
                if (Math.Abs(dot) < Tolerance.dotProduct)
                {
                    this._localZ = val;
                    this._localY = val.Cross(x); // follows right-hand-rule
                }
                
                else
                {
                    throw new System.ArgumentException($"Z-axis is not perpendicular to X-axis. The dot-product is {dot}, but should be 0");
                }
            }
        }

        /// <summary>
        /// Parameterless constructor for serialization.
        /// </summary>
        public FdCoordinateSystem()
        {

        }

        /// <summary>
        /// Construct FdCoordinateSystem from origin point and local x, y, z axes.
        /// </summary>
        public FdCoordinateSystem(FdPoint3d _origin, FdVector3d _localX, FdVector3d _localY, FdVector3d _localZ)
        {
            this.origin = _origin;
            this._localX = _localX;
            this._localY = _localY;
            this._localZ = _localZ;
        }

        #region dynamo
        /// <summary>
        /// Create FdCoordinateSystem from Dynamo coordinate system of a Line or NurbsCurve(?).
        /// This method realignes the coordinate system.
        /// </summary>
        internal static FdCoordinateSystem FromDynamoCoordinateSystemLine(Autodesk.DesignScript.Geometry.CoordinateSystem obj)
        {
            FdPoint3d origin = FdPoint3d.FromDynamo(obj.Origin);
            FdVector3d localX = FdVector3d.FromDynamo(obj.YAxis);
            FdVector3d localY = FdVector3d.FromDynamo(obj.XAxis.Reverse());
            FdVector3d localZ = localX.Cross(localY).Normalize();
            return new FdCoordinateSystem(origin, localX, localY, localZ);
        }

        /// <summary>
        /// 
        /// Create FdCoordinateSystem from Dynamo coordinate system of a Arc or Circle.
        /// Dynamo Arcs and Circles follow left-hand rule.
        /// This method realignes the coordinate system.
        /// </summary>
        internal static FdCoordinateSystem FromDynamoCoordinateSystemArcOrCircle(Autodesk.DesignScript.Geometry.CoordinateSystem obj)
        {
            FdPoint3d origin = FdPoint3d.FromDynamo(obj.Origin);
            FdVector3d localX = FdVector3d.FromDynamo(obj.YAxis);
            FdVector3d localY = FdVector3d.FromDynamo(obj.XAxis);
            FdVector3d localZ = localX.Cross(localY).Normalize();
            return new FdCoordinateSystem(origin, localX, localY, localZ);
        }

        /// <summary>
        /// Create FdCoordinateSystem from Dynamo coordinate system of a Surface.
        /// No realignment neccessary.
        /// </summary>
        internal static FdCoordinateSystem FromDynamoCoordinateSystemSurface(Autodesk.DesignScript.Geometry.CoordinateSystem obj)
        {
            FdPoint3d origin = FdPoint3d.FromDynamo(obj.Origin);
            FdVector3d localX = FdVector3d.FromDynamo(obj.XAxis);
            FdVector3d localY = FdVector3d.FromDynamo(obj.YAxis);
            FdVector3d localZ = FdVector3d.FromDynamo(obj.ZAxis);
            return new FdCoordinateSystem(origin, localX, localY, localZ);
        }

        /// <summary>
        /// Create FdCoordinateSystem from Dynamo coordinate system on curve mid u-point.
        /// </summary>
        internal static FdCoordinateSystem FromDynamoCurve(Autodesk.DesignScript.Geometry.Curve obj)
        {
            // CoordinateSystemAtParameter returns a coordinate system on curve
            // with origin at the point at the given parameter.
            // The XAxis is aligned with the curve normal,
            // the YAxis is aligned with the curve tangent at this point, 
            // and the ZAxis is aligned with the up-vector or binormal at this point
            Autodesk.DesignScript.Geometry.CoordinateSystem cs = obj.CoordinateSystemAtParameter(0.5);

            // Note: Arcs and Circles in Dynamo are defined with left-hand rule while coordinate system is defined by right-hand rule
            if (obj.GetType() == typeof(Autodesk.DesignScript.Geometry.Arc) || obj.GetType() == typeof(Autodesk.DesignScript.Geometry.Circle))
            {
                return FdCoordinateSystem.FromDynamoCoordinateSystemArcOrCircle(cs);
            }
            else
            {
                return FromDynamoCoordinateSystemLine(cs);
            }
        }

        /// <summary>
        /// Create FdCoordinateSystem from Dynamo coordinate system on surface mid u/v-point.
        /// </summary>
        internal static FdCoordinateSystem FromDynamoSurface(Autodesk.DesignScript.Geometry.Surface obj)
        {
            Autodesk.DesignScript.Geometry.CoordinateSystem cs = obj.CoordinateSystemAtParameter(0.5, 0.5);
            return FdCoordinateSystem.FromDynamoCoordinateSystemSurface(cs);
        }
        #endregion

    }
}