using System;
using System.IO;
using System.Linq;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main()
        {
            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = text
                .Split('.')
                .Select(sentence => sentence.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries).ToList())
                .ToList();
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences); 
            while (true)
            {
                Console.Write("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), 11);
                Console.WriteLine(phrase);
            }
        }
    }
}