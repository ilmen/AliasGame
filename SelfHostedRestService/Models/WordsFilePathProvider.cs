using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class WordsFilePathProvider
    {
        public string GetPath()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["WordsFilePath"];

            return "words.txt";
        }
    }
}
