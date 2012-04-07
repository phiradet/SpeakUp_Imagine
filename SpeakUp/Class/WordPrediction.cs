using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpeakUp
{
    class WordPrediction
    {
        public Trie EN_Trie;
        Trie TH_Trie;
        Dictionary<string, double> finalProbTable;
        double MIN_BIGRAM_EN = -8.87472776577;
        double MIN_UNIGRAM_EN = -8.87472776577;
        double DELTA = -13;//Math.Log(double.MinValue);
        const double DUMMY_PROB = 8888;
        const double BIGRAM_W = 0.7;
        const double UNIGRAM_W = (1 - BIGRAM_W);

        public WordPrediction()
        {
            CreateTrie.PrepareTrie(Constants.unigramEN_path, Constants.bigramEN_path, out EN_Trie);
            //CreateTrie.PrepareTrie(Constants.unigramTH_path, Constants.bigramTH_path, out TH_Trie);
        }

        private int CompareProb(string a, string b)
        {
            double probA = returnProb(a);
            double probB = returnProb(b);
            if (probA == probB)
                return 0;
            else if (probA < probB)
                return 1;
            else
                return -1;
        }

        private double returnProb(string item)
        {
            if (this.finalProbTable.ContainsKey(item))
            {
                return finalProbTable[item];
            }
            else
            {
                return DELTA;
            }
        }

        public List<String> Predict(string context)
        {
            List<Trie> biGramTrieLst;
            List<String> wordList;
            List<Double> probUnigram;
            context = context.Trim();

           
            int language = LangUtils.LanguageDetect(context);
            if (language == Constants._engLanguage)
            {
                #region EN_WORDPREDICT
                string[] segmentedWord = LangUtils.ENTWordCut(context);
                int lenSegmented = segmentedWord.Length;
                if (lenSegmented == 0)
                    return new List<string>();
                else if (lenSegmented == 1)
                {
                    string lastWord = segmentedWord[lenSegmented - 1];
                    EN_Trie.SimilarWord(lastWord, out wordList, out probUnigram, out biGramTrieLst);
                    this.finalProbTable = new Dictionary<string, double>();
                    for (int i = 0; i < wordList.Count; i++)
                    {
                        string wordKey = wordList[i];
                        double probVal = probUnigram[i];
                        this.finalProbTable[wordKey] = probVal;
                    }
                    wordList.Sort(CompareProb);
                    return wordList;
                }
                else
                {
                    string lastWord = segmentedWord[lenSegmented - 1];
                    string secondLast = segmentedWord[lenSegmented - 2];
                    EN_Trie.SimilarWord(lastWord, out wordList, out probUnigram, out biGramTrieLst);
                    finalProbTable = new Dictionary<string, double>();
                    for (int i = 0; i < wordList.Count; i++)
                    {
                        string wordKey = wordList[i];
                        double probVal = probUnigram[i];
                        double bigramProb;
                        if (biGramTrieLst[i] == null)
                            bigramProb = DELTA;
                        else{
                            biGramTrieLst[i].ContainStringInTrie(secondLast, out bigramProb);
                            if (bigramProb == DUMMY_PROB)
                                bigramProb = DELTA;
                        }
                        if (wordKey == null)
                            Console.Read();
                        double finalProbVal= (UNIGRAM_W * probVal) + (BIGRAM_W * bigramProb);
                        if (finalProbVal == double.NaN)
                            Console.Read();
                        finalProbTable[wordKey] = finalProbVal;
                    }
                    wordList.Sort(CompareProb);
                    return wordList;
                }
                #endregion
            }
            else if (language == Constants._thaLanguage)
            {
                #region TH_WORDPREDICT
                string[] segmentedWord = LangUtils.THWordCut(context);
                int lenSegmented = segmentedWord.Length;
                if (lenSegmented == 0)
                    return new List<string>();
                else if (lenSegmented == 1)
                {
                    string lastWord = segmentedWord[lenSegmented - 1];
                    TH_Trie.SimilarWord(lastWord, out wordList, out probUnigram, out biGramTrieLst);
                    finalProbTable = new Dictionary<string, double>();
                    for (int i = 0; i < wordList.Count; i++)
                    {
                        string wordKey = wordList[i];
                        double probVal = probUnigram[i];
                        finalProbTable[wordKey] = probVal;
                    }
                    wordList.Sort(CompareProb);
                    return wordList;
                }
                else
                {
                    string lastWord = segmentedWord[lenSegmented - 1];
                    string secondLast = segmentedWord[lenSegmented - 2];
                    TH_Trie.SimilarWord(lastWord, out wordList, out probUnigram, out biGramTrieLst);
                    finalProbTable = new Dictionary<string, double>();
                    for (int i = 0; i < wordList.Count; i++)
                    {
                        string wordKey = wordList[i];
                        double probVal = probUnigram[i];
                        double bigramProb;
                        biGramTrieLst[i].ContainStringInTrie(secondLast, out bigramProb);
                        if (bigramProb == DUMMY_PROB)
                            bigramProb = DELTA;
                        finalProbTable[wordKey] = (UNIGRAM_W * probVal) + (BIGRAM_W * bigramProb);
                    }
                    wordList.Sort(CompareProb);
                    return wordList;
                }
                #endregion
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
