﻿using SelfHostedRestService.Infrastructure;
using System;
using System.IO;
using System.Linq;

namespace SelfHostedRestService.Models
{
    public class Words
    {
        public string[] GetAllWords()
        {
            //return new string[] { "abc", "def" };

            var path = GetPath();
            CreateIfNotExists(path);
            
            var wordsRowText = File.ReadAllText(path);

            return wordsRowText
                .Split(new char[] { ';' })
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
        }

        private void CreateIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "пустой;словарь;");
            }
        }

        private string GetPath()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["WordsFilePath"];

            return "words.txt";
        }
    }
}