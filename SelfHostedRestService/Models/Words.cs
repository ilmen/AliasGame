﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfHostedRestService.Models
{
    public class Words
    {
        public string[] GetAllWords()
        {
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