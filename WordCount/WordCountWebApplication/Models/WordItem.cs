using System.Xml.Serialization;

namespace WordCountWebApplication.Models
{
    [XmlRoot("words")]
    public class Words
    {
        [XmlElement("word")]
        public Word[] WordList { get; set; }
    }

    
    public class Word
    {
        [XmlAttribute("text")]
        public string Text { get; set; }
        [XmlAttribute("count")]
        public int Count { get; set; }
    }
}