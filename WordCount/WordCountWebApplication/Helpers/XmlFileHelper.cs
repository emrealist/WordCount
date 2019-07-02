using System.Linq;
using System.Runtime.Caching;
using System.Xml.Linq;
using System.Xml.Serialization;
using WordCountWebApplication.Models;

namespace WordCountWebApplication
{
    public sealed class XmlFileHelper
    {
        private static MemoryCache _cache = new MemoryCache("WordList");
        private XmlFileHelper()
        {
        }

        public static XmlFileHelper Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        public IOrderedEnumerable<Word> GetWordListFromXmlFile(string xmlFilePath)
        {
            // öncelikle cache bak
            // cachede bulursan onu döndür
            var result = _cache.Get(xmlFilePath);
            if (result != null)
                return result as IOrderedEnumerable<Word>;

            // cachede bulunamadı dosyadan oku 
            var xmlDoc = XDocument.Load(xmlFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(Words));
            using (var reader = xmlDoc.CreateReader())
            {
                // deserialize ile nesneleri oluştur
                var words = serializer.Deserialize(reader) as Words;

                // kelime adedine göre en büyük en başata olacak şekilde sırala
                var query = from worditem in words.WordList
                            orderby worditem.Count descending
                            select worditem;

                //cache ekle
                CacheItemPolicy policy = new CacheItemPolicy();
                _cache.Add(new CacheItem(xmlFilePath, query), policy);

                // sonucu döndür
                return query;
            }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly XmlFileHelper instance = new XmlFileHelper();
        }
    }
}