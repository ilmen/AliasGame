﻿using SelfHostedRestService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class Cards
    {
        int MaxWordsCountInOneCard;

        IShuffler<string> Shuffler;

        ICutter<string> Cutter;

        public Cards(int wordsCountInOneCard, IShuffler<string> shuffler, ICutter<string> cutter)
        {
            if (wordsCountInOneCard <= 0) throw new ArgumentException("Параметр wordsCountInOneCard должен быть больше нуля!");

            this.MaxWordsCountInOneCard = wordsCountInOneCard;

            this.Shuffler = shuffler;

            this.Cutter = cutter;
        }

        public Card[] GetCards(string[] words)
        {
            var activeWords = Cutter.CutMultipleOfBasis(words, MaxWordsCountInOneCard);
            var shuffledWords = Shuffler.Shuffle(activeWords);
            
            var wordGroups = shuffledWords
                .Select((word, index) => new
                {
                    Word = word,
                    CardIndex = (int)(index / MaxWordsCountInOneCard)
                })
                .GroupBy(x => x.CardIndex)
                .ToArray();

            return wordGroups
                .Select(x => new Card(x.Key, x.Select(w => w.Word).ToArray()))
                .ToArray();
        }
    }
}
