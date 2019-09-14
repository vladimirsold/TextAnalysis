using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static int LengthOfParseWord(int index, string sentence)
        {
            var length = 0;
            while (index < sentence.Length && (sentence[index] == '\'' || char.IsLetter(sentence[index])))
            {
                length++;
                index++;
            }
            return length;
        }

        public static List<string> ParseWords(string sentence)
        {
            var words = new List<string>();
            int index = 0, length = 0;
            while (index < sentence.Length)
            {
                if (sentence[index] == '\'' || char.IsLetter(sentence[index]))
                {
                    length = LengthOfParseWord(index, sentence);
                    words.Add(sentence.Substring(index, length).ToLower());
                    index += length;
                }
                else
                {
                    index++;
                }
            }
            return words;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.Split('.', '!', '?', ';', ':', '(', ')', '"');
            foreach (var sentence in sentences)
            {
                var sentenceList = ParseWords(sentence);
                if (sentenceList.Count > 0)
                {
                    sentencesList.Add(sentenceList);
                }
            }
            return sentencesList;
        }
    }
}
