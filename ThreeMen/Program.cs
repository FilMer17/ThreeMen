using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeMen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = ReadFileText("ThreeMenInBoat.txt");
            var words = GetWordsIndexes(lines);

            string newFile = "WordIndexes.txt";

            using (var sw = new StreamWriter(newFile))
            {
                foreach (var word in words.Keys)
                {
                    sw.WriteLine($"{word}: {words[word]}");
                }
            }

            Console.WriteLine($"Vse zapsáno do: {newFile}");
            Console.ReadLine();
        }

        static List<string> ReadFileText(string fromFile)
        {
            List<string> output = new List<string>();
            try
            {
                using (var sr = new StreamReader(fromFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        output.Add(line);
                    }
                }

                return output;
            }
            catch (IOException e)
            {
                Console.WriteLine($"Nelze přečíst soubor: {fromFile}");
                Console.WriteLine(e.Message);
            }

            return new List<string>();
        }

        static Dictionary<string, string> GetWordsIndexes(List<string> lines)
        {
            int lineIndex = 0;
            char[] separators = new char[] { ' ', ',', '.', ';', '-', '—', '_', '!', '?', '“', '”', ':', '(', ')', '\r' };
            string[] banned = new string[] {"a", "the", "an", "and", "aboard","about","above","across","after","against","along","amid","among","anti",
                "around","as","at","before","behind","below","beneath","beside","besides","between","beyond","but","by","concerning",
                "considering","despite","down","during","except","excepting","excluding","following","for","from","in","inside","into",
                "like","minus","near","of","off","on","onto","opposite","outside","over","past","per","plus","regarding","round","save",
                "since","than","through","to","toward","towards","under","underneath","unlike","until","up","upon","versus","via","with",
                "within","without",};

            Dictionary<string, string> hashWords = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                lineIndex++;
                string[] wordsInLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in wordsInLine)
                {
                    if (!banned.Contains(word))
                    {
                        if (!hashWords.ContainsKey(word))
                        {
                            hashWords.Add(word, Convert.ToString(lineIndex) + ", ");
                        }
                        else
                        {
                            string temp = hashWords[word];
                            hashWords[word] = temp + Convert.ToString(lineIndex) + ", ";
                        }
                    }
                }
            }

            return hashWords;
        }
    }
}
