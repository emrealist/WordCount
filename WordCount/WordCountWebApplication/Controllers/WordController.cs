using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WordCountWebApplication.Models;

namespace WordCountWebApplication.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // dosyanın sunucudaki fiziksel yolunu bul
            var xmlFilePath = Server.MapPath(@"~/App_Data/result.xml");
            // kelime listesini yardımcı sınıf aracılığı ile al
            var wordList = XmlFileHelper.Instance.GetWordListFromXmlFile(xmlFilePath);
            // gelen listeden ilk 10 kelimeyi al
            IEnumerable<Word> model = wordList.Take(10);
            // view'e geri döndür
            return View(model);
        }

    }
}