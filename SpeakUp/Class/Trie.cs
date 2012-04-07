using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SpeakUp
{
    [Serializable()]
    public class Trie
    {
        const double dummyProb = 8888;
        double prob = dummyProb;
        Trie nextGram = null;
        bool isTerminate = false;

        public Dictionary<char, Trie> root = new Dictionary<char, Trie>();

        public Trie()
        {
        }

        public Trie(SerializationInfo info, StreamingContext ctxt)
        {
            this.root = (Dictionary<char, Trie>)info.GetValue("root", typeof(Dictionary<char, Trie>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("root", this.root);
        }

        public Trie(double prob, Trie nextGram, bool isTerminate)
        {
            this.prob = prob;
            this.nextGram = nextGram;
            this.isTerminate = isTerminate;
        }

        public bool isRoot(char c)
        {
            return root.ContainsKey(c);
        }
        public Trie GetAllChild(char c)
        {
            if (!isRoot(c))
                return null;
            else
                return root[c];
        }
        public void InsertString(string s, double prob, Trie nextGram)
        {
            if (s.Length == 0)
            {
                if (!isRoot('\0'))
                    root['\0'] = new Trie(prob, nextGram, true);
            }
            else
            {
                if (isRoot(s[0]))
                {
                    root[s[0]].InsertString(s.Substring(1), prob, nextGram);
                }
                else
                {
                    root[s[0]] = new Trie();
                    root[s[0]].InsertString(s.Substring(1), prob, nextGram);
                }
            }
        }
        public bool ContainStringInTrie(string s, out double prob, out Trie nextGram)
        {
            if (s.Length == 0)
            {
                if (isRoot('\0'))
                {
                    prob = root['\0'].prob;
                    nextGram = root['\0'].nextGram;
                    return true;
                }
                else
                {
                    prob = dummyProb;
                    nextGram = null;
                    return false;
                }
            }
            else
            {
                if (!isRoot(s[0]))
                {
                    prob = dummyProb;
                    nextGram = null;
                    return false;
                }
                else
                    return root[s[0]].ContainStringInTrie(s.Substring(1), out prob, out nextGram);
            }
        }

        public bool ContainStringInTrie(string s, out double prob)
        {
            if (s.Length == 0)
            {
                if (isRoot('\0'))
                {
                    prob = root['\0'].prob;
                    return true;
                }
                else
                {
                    prob = dummyProb;
                    return false;
                }
            }
            else
            {
                if (!isRoot(s[0]))
                {
                    prob = dummyProb;
                    return false;
                }
                else
                    return root[s[0]].ContainStringInTrie(s.Substring(1), out prob);
            }
        }

        public void SimilarWord(string s, out List<string> strList, out List<double> probList, out List<Trie> nextGramList)
        {
            Trie subT = GetSubT(s);
            if (subT == null)
            {
                strList = new List<string>();
                probList = new List<double>();
                nextGramList = new List<Trie>();
                return;// new List<string>();
            }
            List<string> w;
            List<double> p;
            List<Trie> t;
            DFS(subT, out w, out p, out t);
            strList = new List<string>();
            probList = new List<double>();
            nextGramList = new List<Trie>();
            for (int n = 0; n < w.Count; n++)
            {
                string tmpW = w[n];
                double tmpP = p[n];
                Trie tmpT = t[n];
                if (s + tmpW != s)          //If you want similar word please comment this contion
                {
                    strList.Add(s + tmpW);
                    probList.Add(tmpP);
                    nextGramList.Add(tmpT);
                }
            }
        }
        private Trie GetSubT(string s)
        {
            if (!isRoot(s[0]))
                return null;
            else
            {
                if (s.Length == 1)
                    return root[s[0]];
                else
                    return root[s[0]].GetSubT(s.Substring(1));
            }
        }
        public Dictionary<char, Trie>.KeyCollection GetAllRootSymbol()
        {
            return root.Keys;
        }
        private void DFS(Trie t, out List<string> wordList, out List<double> probList, out List<Trie> nextGramList)
        {
            List<string> ans = new List<string>();
            List<double> ansP = new List<double>();
            List<Trie> ansT = new List<Trie>();
            foreach (KeyValuePair<char, Trie> entry in t.root)
            {
                Trie val = entry.Value;
                char key = entry.Key;
                if (key == '\0')
                {
                    if (ans.Contains(""))
                        continue;
                    else
                    {
                        ans.Add("");
                        ansP.Add(val.prob);
                        ansT.Add(val.nextGram);
                    }
                }
                else
                {
                    List<string> tmpAns = new List<string>();
                    List<double> tmpP = new List<double>();
                    List<Trie> tmpT = new List<Trie>();
                    DFS(t.GetAllChild(key), out tmpAns, out tmpP, out tmpT);
                    int k = 0;
                    foreach (string currSuffix in tmpAns)
                    {
                        ans.Add(key + currSuffix);
                        ansP.Add(tmpP[k]);
                        ansT.Add(tmpT[k++]);
                    }
                }
            }
            wordList = ans;
            probList = ansP;
            nextGramList = ansT;
        }
    }
}
