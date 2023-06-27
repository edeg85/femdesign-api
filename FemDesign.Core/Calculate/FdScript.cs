// https://strusoft.com/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FemDesign.Results;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

namespace FemDesign.Calculate
{
    public static class Extension
    {
        public static XElement ToXElement<T>(this object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj, ns);
                    return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }
        }

        public static T FromXElement<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }


    /// <summary>
    /// Base class for all commands that can be run in FEM-Design
    /// </summary>
    [System.Serializable]
    public abstract partial class CmdCommand
    {
        public abstract XElement ToXElement();

    }

    /// <summary>
    /// Fdscript root class
    /// </summary>
    public partial class FdScript
    {

        public static readonly string Version = "2200";

        [XmlIgnore]
        public List<CmdCommand> Commands = new List<CmdCommand>();

        public FdScript(string logFilePath, params CmdCommand[] commands)
        {
            Header = new FdScriptHeader(logFilePath);
            Commands = commands.ToList();
        }

        public FdScript(string logFilePath, List<CmdCommand> commands)
        {
            Header = new FdScriptHeader(logFilePath);
            Commands = commands;
        }

        public void Add(CmdCommand command)
        {
            Commands.Add(command);
        }

        public void Serialize(string path)
        {
            XDocument doc = new XDocument();

            var root = new XElement(
                "fdscript",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")
                );

            root.Add(Header.ToXElement());

            Commands.ForEach(c => root.Add(c.ToXElement()));

            doc.Add(root);
            doc.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
            doc.Save(path);
        }

        /// <summary>
        /// Serialize Model to string.
        /// </summary>
        public string SerializeToString(string fdScriptPath)
        {
            Serialize(fdScriptPath);
            var xmlLines = System.IO.File.ReadAllLines(fdScriptPath);
            string xmlText = string.Join("\n", xmlLines);
            return xmlText;
        }
    }
}
