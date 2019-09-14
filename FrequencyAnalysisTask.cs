using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();
            var bigrams = GetNgramsFromText(2, text);
            var threegrams = GetNgramsFromText(3, text);
            AddNgramsInDictionary(frequencyDictionary, bigrams);
            AddNgramsInDictionary(frequencyDictionary, threegrams);
            var mostFreqeuntNextWords = new Dictionary<string, string>();
            foreach(var nGramms in frequencyDictionary)
            {
                var key = nGramms.Key;
                var value = GetFrequentValueOfEndGramm(frequencyDictionary[key]);
                mostFreqeuntNextWords[key] = value;
            }
            return mostFreqeuntNextWords;
        }

        private static List<List<string>> GetNgramsFromText(int sizeOfNGrams, List<List<string>> text)
        {
            var nGrams = new List<List<string>>();
            foreach(var sentence in text)
            {
                if(sentence.Count >= sizeOfNGrams)
                {
                    for(var i = 0; i <= sentence.Count - sizeOfNGrams; i++)
                    {
                        var nGram = sentence.GetRange(i, sizeOfNGrams);
                        nGrams.Add(nGram);
                    }
                }
            }
            return nGrams;
        }

        private static void AddNgramsInDictionary(Dictionary<string, Dictionary<string, int>> frequencyDictionary, List<List<string>> nGrams)
        {
            foreach(var nGram in nGrams)
            {
                var beginingNgram = String.Join(" ", nGram.GetRange(0, nGram.Count - 1));
                var endNgram = nGram[nGram.Count - 1];
                if(!frequencyDictionary.ContainsKey(beginingNgram))
                {
                    frequencyDictionary[beginingNgram] = new Dictionary<string, int>(1)
                    {
                        [endNgram] = 0
                    };
                }
                if(!frequencyDictionary[beginingNgram].ContainsKey(endNgram))
                {
                    frequencyDictionary[beginingNgram][endNgram] = 0;
                }
                frequencyDictionary[beginingNgram][endNgram]++;
            }
        }
        private static string GetFrequentValueOfEndGramm(Dictionary<string, int> dictionary)
        {
            var max = dictionary.Values.Max();
            var allEndsNGramWithMaxEntry = from entry in dictionary
                                           where entry.Value == max
                                           select entry.Key;
            var frequentValueOfendNGram = GetMinLexOfNGramm(allEndsNGramWithMaxEntry.ToList());
            return frequentValueOfendNGram;
        }

        public static string GetMinLexOfNGramm(List<string> listOfNGramm)
        {
            string minLexEndOfNGramm = listOfNGramm[0];
            if(listOfNGramm.Count == 1)
            {
                return minLexEndOfNGramm;
            }
            else
            {
                foreach(var endOfNGramm in listOfNGramm)
                {
                    minLexEndOfNGramm = string.CompareOrdinal(minLexEndOfNGramm, endOfNGramm) > 0
                        ? endOfNGramm : minLexEndOfNGramm;
                }
            }
            return minLexEndOfNGramm;
        }
    }
}