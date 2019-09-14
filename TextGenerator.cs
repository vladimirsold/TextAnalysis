using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGenerator
    {
        static string GetNextWord(List<string> phrase, Dictionary<string, string> nextWords)
        {
            int count = phrase.Count;
            string key;
            if (count > 1)
            {
                key = phrase[count - 2] + " " + phrase[count - 1];
                if (nextWords.ContainsKey(key))
                {
                    return nextWords[key];
                }
            }
            key = phrase[count - 1];
            if (nextWords.ContainsKey(key))
            {
                return nextWords[key];
            }
            else
            {
                return null;
            }
        }

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var phrase = phraseBeginning.Split(' ').ToList();
            for (var addedWord = 0; addedWord < wordsCount; addedWord++)
            {
                var nextWord = GetNextWord(phrase, nextWords);
                if (nextWord != null)
                {
                    phrase.Add(nextWord);
                }
                else
                {
                    break;
                }
            }
            return string.Join(" ", phrase);
        }
    }
}
