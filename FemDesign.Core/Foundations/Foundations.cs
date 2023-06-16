using FemDesign.Supports;
using StruSoft.Interop.StruXml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FemDesign.Foundations
{
    public partial class Foundations
    {
        private List<StruSoft.Interop.StruXml.Data.Foundation_type> store;
        public List<StruSoft.Interop.StruXml.Data.Foundation_type> Store
        {
            get { return this.store; }
        }

        public Foundations(List<StruSoft.Interop.StruXml.Data.Foundation_type> obj)
        {
            this.store = obj;
        }

        public List<IsolatedFoundation> IsolatedFoundations
        {
            get
            {
                var obj = this.store.OfType<StruSoft.Interop.StruXml.Data.Ptfoundation_type>().ToList();
                var isolatedFoundation = new List<IsolatedFoundation>();
                foreach ( var item in obj )
                    isolatedFoundation.Add( new IsolatedFoundation(item));
                return isolatedFoundation;
            }
        }
        public List<Lnfoundation_type> wall_foundationField
        {
            get
            {
                return this.store.OfType<StruSoft.Interop.StruXml.Data.Lnfoundation_type>().ToList();
            }
        }
        public List<Sffoundation_type> foundation_slabField
        {
            get
            {
                return this.store.OfType<StruSoft.Interop.StruXml.Data.Sffoundation_type>().ToList();
            }
        }


        public List<dynamic> GetFoundations()
        {
            var objs = new List<dynamic>();
            objs.AddRange(this.IsolatedFoundations);
            objs.AddRange(this.wall_foundationField); // to implement
            objs.AddRange(this.foundation_slabField); // to implement
            return objs;
        }
    }
}