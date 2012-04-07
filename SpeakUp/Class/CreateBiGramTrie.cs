using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SpeakUp
{
    class CreateTrie
    {
        struct bigramData
        {
            public Dictionary<string, double> biProb;
        }
        private static Dictionary<string, double> allWord;
        private static Dictionary<string, bigramData> allWord_Bigram;

        public static void PrepareTrie(string pathUnigram, string pathBigram, out Trie outTrie)
        {
            allWord = new Dictionary<string, double>();
            allWord_Bigram = new Dictionary<string, bigramData>();
            LoadAllWord(pathUnigram);
            LoadAllWord_Bigram(pathBigram);

            Trie en = new Trie();
            Console.WriteLine("start trie");
            foreach (KeyValuePair<string, double> entry in allWord)
            {
                string word = entry.Key;
                double prob = entry.Value;
                Dictionary<string, double> biProb;
                if (allWord_Bigram.ContainsKey(word))
                {
                    biProb = allWord_Bigram[word].biProb;
                    Trie biGramTri = new Trie();
                    foreach (KeyValuePair<string, double> entryBigram in biProb)
                    {
                        string wordBi = entryBigram.Key;
                        double probBi = entryBigram.Value;
                        biGramTri.InsertString(wordBi, probBi, null);
                    }
                    en.InsertString(word, prob, biGramTri);
                }
                else
                {
                    en.InsertString(word, prob, null);
                }


            }
            outTrie = en;
        }

        static void LoadAllWord_Bigram(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path, Encoding.UTF8);
            string currWord = "";
            string currFront = "";
            double currProb = 0.0f;
            for (int i = 0; i < lines.Length; i++)
            {
                currWord = lines[i].Trim().Split('\t')[0];
                currFront = lines[i].Trim().Split('\t')[1];
                currProb = double.Parse(lines[i].Trim().Split('\t')[2]);
                if (!allWord_Bigram.ContainsKey(currWord))
                {
                    bigramData tmp = new bigramData();
                    tmp.biProb = new Dictionary<string, double>();
                    allWord_Bigram.Add(currWord, tmp);
                }
                allWord_Bigram[currWord].biProb.Add(currFront, currProb);
            }
        }
        static void LoadAllWord(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path, Encoding.UTF8);
            string currWord = "";
            double currProb = 0.0f;
            for (int i = 0; i < lines.Length; i++)
            {
                currWord = lines[i].Trim().Split('\t')[0];
                currProb = double.Parse(lines[i].Trim().Split('\t')[1]);
                allWord.Add(currWord, currProb);
            }
            //Console.WriteLine("finished merger all:{0}", allWord.Capacity);
        }
    }
}
