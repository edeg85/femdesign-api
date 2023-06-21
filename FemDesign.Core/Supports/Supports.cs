// https://strusoft.com/

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FemDesign.Supports
{
    /// <summary>
    /// supports
    /// </summary>
    [System.Serializable]
    public partial class Supports
    {
        internal StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports store;

        internal Supports()
        {
            this.store = new StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports();
        }

        internal Supports(StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports obj)
        {
            this.store = obj;
        }

        //[XmlElement("point_support", Order = 1)]
        //public List<PointSupport> PointSupport = new List<PointSupport>();

        public List<PointSupport> PointSupport
        {
            get
            {
                if (this.store.Point_support == null)
                    this.store.Point_support = new List<StruSoft.Interop.StruXml.Data.Point_support_type>();

                return this.store.Point_support.Select(x => new PointSupport(x) ).ToList();
            }
            set { this.store.Point_support = value.Select(x => x.store).ToList(); }
        }

        [XmlElement("line_support", Order = 2)]
        public List<LineSupport> LineSupport = new List<LineSupport>(); // line_support_type
        [XmlElement("surface_support", Order = 3)] 
        public List<SurfaceSupport> SurfaceSupport = new List<SurfaceSupport>(); // surface_support
        [XmlElement("stiffness_point", Order = 4)]
        public List<StiffnessPoint> StiffnessPoint = new List<StiffnessPoint>(); // surface_support

        public List<GenericClasses.ISupportElement> GetSupports()
        {
            var objs = new List<GenericClasses.ISupportElement>();
            objs.AddRange(this.PointSupport);
            objs.AddRange(this.LineSupport);
            objs.AddRange(this.SurfaceSupport);
            return objs;
        }

    }
}