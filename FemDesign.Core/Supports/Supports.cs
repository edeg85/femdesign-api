// https://strusoft.com/

using StruSoft.Interop.StruXml.Data;
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
        private StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports store;
        public StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports Store
        {
            get { return this.store; }
        }

        public Supports(StruSoft.Interop.StruXml.Data.DatabaseEntitiesSupports supports)
        {
            this.store = supports;
        }

        public List<PointSupport> PointSupport
        {
            get { return this.store.Point_support.Select(x => new PointSupport(x)).ToList() ; }
        }
        public List<LineSupport> LineSupport
        {
            get { return this.store.Line_support.Select(x => new LineSupport(x)).ToList(); }
        }
        public List<SurfaceSupport> SurfaceSupport
        {
            get { return this.store.Surface_support.Select(x => new SurfaceSupport(x)).ToList(); }
        }
        public List<StiffnessPoint> StiffnessPoint
        {
            get { return this.store.Stiffness_point.Select(x => new StiffnessPoint(x)).ToList(); }
        }

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