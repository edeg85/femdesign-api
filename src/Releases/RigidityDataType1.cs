// https://strusoft.com/

using System.Xml.Serialization;

#region dynamo
using Autodesk.DesignScript.Runtime;
#endregion

namespace FemDesign.Releases
{
    /// <summary>
    /// rigidity_data_type1
    /// </summary>
    [System.Serializable]
    [IsVisibleInDynamoLibrary(false)]
    public class RigidityDataType1
    {
        [XmlElement("motions", Order=1)]
        public Releases.Motions motions { get; set; }
        [XmlElement("plastic_limit_forces", Order=2)]
        public Releases.PlasticityType3d plasticLimitForces { get; set; }
        [XmlAttribute("detach")]
        public string _detach; // detach_type
        [XmlIgnore]
        public string detach
        {
            get {return this._detach;}
            set {this._detach = RestrictedString.DetachType(value);}

        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public RigidityDataType1()
        {

        }

        /// <summary>
        /// Construct RigidityDataType1 with motions only
        /// </summary>
        public RigidityDataType1(Motions motions)
        {
            this.motions = motions;
        }
    }
}