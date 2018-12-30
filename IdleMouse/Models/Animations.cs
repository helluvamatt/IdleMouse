using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace IdleMouse.Models
{
    public class AnimationsConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            using (var reader = new XmlNodeReader(section))
            {
                return Parse(reader);
            }
        }

        public static AnimationCollection LoadFromFile(string path)
        {
            using (var reader = new XmlTextReader(new System.IO.StreamReader(path)))
            {
                return Parse(reader);
            }
        }

        private static AnimationCollection Parse(XmlReader reader)
        {
            var serializer = new XmlSerializer(typeof(AnimationCollection), "http://schneenet.com/IdleMouse/Animations.xsd");
            return serializer.Deserialize(reader) as AnimationCollection;
        }

        public static void SaveToFile(string path, AnimationCollection collection)
        {
            using (var writer = new XmlTextWriter(new System.IO.StreamWriter(path)) { Formatting = Formatting.Indented, Indentation = 2, IndentChar = ' ' })
            {
                var serializer = new XmlSerializer(typeof(AnimationCollection), "http://schneenet.com/IdleMouse/Animations.xsd");
                serializer.Serialize(writer, collection);
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/IdleMouse/Animations.xsd")]
    [XmlRoot(Namespace="http://schneenet.com/IdleMouse/Animations.xsd", ElementName = "animations", IsNullable=false)]
    public class AnimationCollection
    {
        [XmlElement("animation")]
        public Animation[] Animations { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/IdleMouse/Animations.xsd")]
    public class Animation
    {
        [XmlElement("path", typeof(AnimationPath))]
        [XmlElement("ellipse", typeof(AnimationEllipse))]
        public object Item { get; set; }

        [XmlIgnore]
        public Type ItemType
        {
            get => Item?.GetType();
            set
            {
                if (value == null || value == ItemType) return;
                if (value == typeof(AnimationPath))
                {
                    Item = new AnimationPath();
                }
                else if (value == typeof(AnimationEllipse))
                {
                    Item = new AnimationEllipse();
                }
                else
                {
                    throw new ArgumentException("Invalid item type");
                }
            }
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("interpolation")]
        [DefaultValue("Linear")]
        public string Interpolation
        {
            get => EasingFunction.ToString();
            set
            {
                Easings.Functions fn;
                if (!Enum.TryParse(value, out fn)) fn = Easings.Functions.Linear;
                EasingFunction = fn;
            }
        }

        [XmlIgnore]
        public Easings.Functions EasingFunction { get; set; }

        [XmlAttribute("reverse")]
        [DefaultValue(false)]
        public bool Reverse { get; set; }

        [XmlIgnore]
        public InterpolationMode? PathInterpolationMode
        {
            get => (Item as AnimationPath)?.InterpolationMode;
            set
            {
                AnimationPath path;
                if (value.HasValue && (path = Item as AnimationPath) != null)
                {
                    path.InterpolationMode = value.Value;
                }
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/IdleMouse/Animations.xsd")]
    public class AnimationPath
    {
        [XmlIgnore]
        public IList<AnimationPathPoint> PointList { get; private set; } = new List<AnimationPathPoint>();

        [XmlElement("point")]
        public AnimationPathPoint[] Points
        {
            get => PointList.ToArray();
            set => PointList = new List<AnimationPathPoint>(value);
        }

        [XmlAttribute("interpolationMode")]
        [DefaultValue(InterpolationMode.Normal)]
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.Normal;
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://schneenet.com/IdleMouse/Animations.xsd")]
    public enum InterpolationMode
    {
        Normal,
        Alternate,
        Repeat,
    }

    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/IdleMouse/Animations.xsd")]
    public class AnimationPathPoint
    {
        [XmlAttribute("x")]
        public float X { get; set; }

        [XmlAttribute("y")]
        public float Y { get; set; }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/IdleMouse/Animations.xsd")]
    public class AnimationEllipse
    {
        [XmlAttribute("cx")]
        public float CX { get; set; } = 0.5f;
        
        [XmlAttribute("cy")]
        public float CY { get; set; } = 0.5f;

        [XmlAttribute("rx")]
        public float RX { get; set; } = 0.5f;

        [XmlAttribute("ry")]
        public float RY { get; set; } = 0.5f;
    }
}
