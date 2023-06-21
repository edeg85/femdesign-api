using FemDesign;
using FemDesign.Geometry;
using FemDesign.Supports;
using StruSoft.Interop.StruXml.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemDesign
{
    public partial class Database
    {
        private StruSoft.Interop.StruXml.Data.Database store;

        public void Initialise()
        {
            this.store = new StruSoft.Interop.StruXml.Data.Database();
            this.store.Entities = new DatabaseEntities();
            this.store.Sections = new DatabaseSections();
            this.store.Materials = new DatabaseMaterials();
        }

        public Database(Country country)
        {
            this.Initialise();
            this.store.Country = (Eurocodetype)EnumConverter.ConvertEnum(country);
        }

        public void AddPointSupport(PointSupport ptSupport)
        {
            // check if support is in model

            //else
            this.store.Entities.Supports.Point_support.Add(ptSupport.store);
        }

        public void AddPointSupports(List<PointSupport> ptSupport)
        {
            // check if support is in model

            //else
            this.store.Entities.Supports.Point_support.AddRange(ptSupport.Select(x => x.store));
        }

        public void AddLineSupport(LineSupport lnSupport)
        {
            // check if line is in model

            //else
            this.store.Entities.Supports.Line_support.Add(lnSupport.store);
        }


        public void AddPointLoad()
        {

        }

        public void AddLoadCase(FemDesign.Loads.LoadCase ldCase)
        {
            this.store.Entities.Loads.Load_case.Add(ldCase.store);
        }

    }


    public partial class PointSupport
    {
        internal StruSoft.Interop.StruXml.Data.Point_support_type store;
        public PointSupport(Point3d point, bool tx, bool ty, bool tz, bool rx, bool ry, bool rz, string identifier)
        {
            this.Initialise();

            this.store.Name = identifier;
            this.store.Position = new Point_type_3d(point); // use explicit operator or constructor?

            this.store.Item = new StruSoft.Interop.StruXml.Data.Support_rigidity_data_typeGroup();
        }

        public PointSupport(Point3d point, Group group, string identifier)
        {
            this.Initialise();
            this.store.Name = identifier;
        }

        private void Initialise()
        {
            this.store = new Point_support_type();
            this.store.Guid = System.Guid.NewGuid().ToString();
        }
    }

    public partial class LineSupport
    {
        internal StruSoft.Interop.StruXml.Data.Line_support_type store;
        public LineSupport()
        {
            var pt = new Line_support_type();
            this.store = pt;
        }
    }

    public static class EnumConverter
    {
        public static object ConvertEnum(Country value)
        {
            switch (value)
            {
                case Country.S:
                    return StruSoft.Interop.StruXml.Data.Eurocodetype.S;
                case Country.N:
                    return StruSoft.Interop.StruXml.Data.Eurocodetype.N;
                case Country.DK:
                    return StruSoft.Interop.StruXml.Data.Eurocodetype.N;
                default:
                    throw new ArgumentException("Invalid value");
            }
        }

        public static object ConvertEnum(FemDesign.Bars.BarType value)
        {
            switch (value)
            {
                case FemDesign.Bars.BarType.Beam:
                    return StruSoft.Interop.StruXml.Data.Beamtype.Beam;
                case FemDesign.Bars.BarType.Column:
                    return StruSoft.Interop.StruXml.Data.Beamtype.Column;
                case FemDesign.Bars.BarType.Truss:
                    return StruSoft.Interop.StruXml.Data.Beamtype.Truss;
                default:
                    throw new ArgumentException("Invalid value");
            }
        }
    }
}

namespace StruSoft.Interop.StruXml.Data
{
    public partial class Point_type_3d
    {
        public Point_type_3d (Point3d pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
            this.Z = pt.Z;
        }
    }

    public partial class Point_support_type
    {
        internal Point_support_type(FemDesign.PointSupport pt, string name)
        {
            this.Position = pt.store.Position;
            this.Name = name;

            this.Guid = System.Guid.NewGuid().ToString();
            this.Item = pt.store.Item;
        }
    }
}
