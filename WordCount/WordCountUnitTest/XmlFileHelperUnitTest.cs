using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WordCountWebApplication;

namespace WordCountUnitTest
{
    [TestClass]
    public class XmlFileHelperUnitTest
    {

        [TestMethod]
        public void GetWordListFromXmlFileTest_Result_Must_Not_Null()
        {
            var xmlFilePath = @"..\..\..\WordCountWebApplication\App_Data\result.xml";

            // xml dosyadan çeriği okuyan nesnenin instance'ı alınır
            // singleton olduğu için program genelinde sadece tek instance'i olur
            var xmlFileHelper = XmlFileHelper.Instance;
            var result = xmlFileHelper.GetWordListFromXmlFile(xmlFilePath);
            // xml dosyadan içerik okunur ve sonuç null gelirse unit test başarısız olur
            // sonuç hiç bir zaman null olmamalı
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWordListFromXmlFileTest_Result_List_Count_Must_Not_Be_Zero()
        {
            var xmlFilePath = @"..\..\..\WordCountWebApplication\App_Data\result.xml";

            var xmlFileHelper = XmlFileHelper.Instance;
            var result = xmlFileHelper.GetWordListFromXmlFile(xmlFilePath);
            // xml dosyadan içerik okunur ve sonuç liste gelmeli
            // listede eleman adedi hiç bir zaman sıfır olmamalı
            Assert.AreNotSame(0, result.Count());
        }


    }
}
