using SelfHostedRestService.Infrastructure;
using System;
using System.Linq;

namespace SelfHostedRestService.Models
{
    public class Words
    {
        public string[] GetAllWords()
        {
            //return new string[] { "abc", "def" };

            var path = (new WordsFilePathProvider()).GetPath();
            var wordsRowText = System.IO.File.ReadAllText(path);

            return wordsRowText
                .Split(new char[] { ';' })
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
        }
    }
}