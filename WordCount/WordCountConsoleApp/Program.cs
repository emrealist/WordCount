using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace WordCountConsoleApp
{
    class Program
    {
        private readonly static string BookUlr = @"http://www.gutenberg.org/files/2701/2701-0.txt";

        static void Main(string[] args)
        {
            // Instantinate WebClient for download file from url
            var webClient = new WebClient();
            // download book content as a string
            var bookContent = webClient.DownloadString(BookUlr);
            // split content to lines
            var bookLines = bookContent.Split(Environment.NewLine.ToCharArray());

            var separators = new char[] { ' ', '!', '"', '#', '%', '&', '\'', '(', ')', '*', ',', '-', '.', '/', ':', ';', '?', '@', '[', '\\', ']', '_', '{', '}' };

            var query = from line in bookLines
                        from word in line.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                        group word by word into wg
                        select new XElement("word"
                        , new XAttribute("text", wg.Key)
                        , new XAttribute("count", wg.Count())
                        );

            var rootElement = new XElement("words", query);

            var doc = new XDocument(rootElement);

            doc.Save(@"..\..\..\WordCountWebApplication\App_Data\result.xml");

            Console.WriteLine("Done.");
        }
    }
}
