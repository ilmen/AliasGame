using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class Words
    {
        public string[] GetAllWords()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["WordsFilePath"];
            var wordsRowText = System.IO.File.ReadAllText(path);
            return wordsRowText.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}