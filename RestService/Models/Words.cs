﻿using System;
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
            var wordsRowText = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);

            return wordsRowText
                .Split(new char[] { ';' })
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
        }
    }
}