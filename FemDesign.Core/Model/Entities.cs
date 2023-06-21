// https://strusoft.com/

using System.Collections.Generic;
using System.Xml.Serialization;

namespace FemDesign
{
    /// <summary>
    /// entities.
    /// </summary>
    [System.Serializable]
    public partial class Entities
    {
        internal StruSoft.Interop.StruXml.Data.DatabaseEntities store;

        internal Entities()
        {
            this.store = new StruSoft.Interop.StruXml.Data.DatabaseEntities();
        }

        internal Entities(StruSoft.Interop.StruXml.Data.DatabaseEntities db)
        {
            this.store = db;
        }

        [XmlElement("foundations", Order = 1)]
        public Foundations.Foundations Foundations { get; set; } = new Foundations.Foundations();

        [XmlElement("soil_elements", Order = 2)]
        public FemDesign.Soil.SoilElements SoilElements { get; set; }

        [XmlElement("bar", Order = 3)]
        public List<Bars.Bar> Bars { get; set; } = new List<Bars.Bar>();

        [XmlElement("column_corbel", Order = 4)]
        public List<Bars.ColumnCorbel> ColumnCorbels { get; set; } = new List<Bars.ColumnCorbel>();

        [XmlElement("hidden_bar", Order = 5)]
        public List<Reinforcement.HiddenBar> HiddenBars { get; set; } = new List<Reinforcement.HiddenBar>();

        [XmlElement("bar_reinforcement", Order = 6)]
        public List<Reinforcement.BarReinforcement> BarReinforcements { get; set; } = new List<Reinforcement.BarReinforcement>();

        [XmlElement("slab", Order = 7)]
        public List<Shells.Slab> Slabs { get; set; } = new List<Shells.Slab>();

        [XmlElement("shell_buckling", Order = 8)]
        public List<Shells.ShellBucklingType> ShellBucklings { get; set; } = new List<Shells.ShellBucklingType>();

        [XmlElement("wall_corbel", Order = 9)]
        public List<Shells.WallCorbel> WallCorbel { get; set; } = new List<Shells.WallCorbel>();

        [XmlElement("surface_reinforcement_parameters", Order = 10)]
        public List<Reinforcement.SurfaceReinforcementParameters> SurfaceReinforcementParameters { get; set; } = new List<Reinforcement.SurfaceReinforcementParameters>();

        [XmlElement("surface_reinforcement", Order = 11)]
        public List<Reinforcement.SurfaceReinforcement> SurfaceReinforcements { get; set; } = new List<Reinforcement.SurfaceReinforcement>();

        [XmlElement("punching_area", Order = 12)]
        public List<Reinforcement.PunchingArea> PunchingArea { get; set; } = new List<Reinforcement.PunchingArea>();

        [XmlElement("punching_reinforcement", Order = 13)]
        public List<Reinforcement.PunchingReinforcement> PunchingReinforcements { get; set; } = new List<Reinforcement.PunchingReinforcement>();

        [XmlElement("no-shear_region", Order = 14)]
        public List<Reinforcement.NoShearRegionType> NoShearRegions { get; set; } = new List<Reinforcement.NoShearRegionType>();

        [XmlElement("shear_control_region", Order = 15)]
        public List<Reinforcement.ShearControlRegionType> NoShearControlRegions { get; set; } = new List<Reinforcement.ShearControlRegionType>();

        [XmlElement("panel", Order = 16)]
        public List<Shells.Panel> Panels { get; set; } = new List<Shells.Panel>();

        [XmlElement("post-tensioned_cable", Order = 17)]
        public List<Reinforcement.Ptc> PostTensionedCables { get; set; } = new List<Reinforcement.Ptc>();

        [XmlElement("loads", Order = 18)]
        public Loads.Loads Loads { get; set; } = new Loads.Loads();

        //[XmlElement("supports", Order = 19)]
        //public Supports.Supports Supports { get; set; } = new Supports.Supports();

        public Supports.Supports Supports
        {
            get
            {
                return new Supports.Supports(this.store.Supports);
            }
            set
            {
                this.store.Supports = value.store;
            }
        }

        [XmlElement("advanced-fem", Order = 20)]
        public AdvancedFem AdvancedFem { get; set; } = new AdvancedFem();

        [XmlElement("storeys", Order = 21)]
        public StructureGrid.Storeys Storeys { get; set; }

        [XmlElement("axes", Order = 22)]
        public StructureGrid.Axes Axes { get; set; }

        [XmlElement("labelled_sections_geometry", Order = 23)]
        public AuxiliaryResults.LabelledSectionsGeometry LabelledSections;

        [XmlElement("result_points", Order = 24)]
        public AuxiliaryResults.ResultPointsGeometry ResultPoints;

        // ref planes
        // tsolids
        // regions
    }
}