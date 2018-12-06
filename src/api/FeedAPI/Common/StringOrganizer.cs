using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeedAPI.Common
{
    public class StringOrganizer : IDisposable
    {
        private Dictionary<string, int> _wordCounter;
        private Dictionary<string, int> _tempCounter;

        public StringOrganizer()
        {
            this._wordCounter = new Dictionary<string, int>();
            this._tempCounter = new Dictionary<string, int>();
        }

        public List<string> GetTotalTopWords(int quantity)
        {
            return _wordCounter.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(quantity).Select(x => x.Key).ToList();
        }

        public int GetTotalPerWord(string word)
        {
            return this._wordCounter[word];
        }
        
        public int GetTempWordsSum()
        {
            return this._tempCounter.Select(x => x.Value).Sum();
        }

        public void ClearTemp()
        {
            this._tempCounter = new Dictionary<string, int>();
        }

        public void CountWords(string p_Text)
        {
            List<string> prohibitedWords = new List<string>();

            LoadArticles(prohibitedWords);
            LoadPrepositions(prohibitedWords);
            LoadReservedString(prohibitedWords);

            string[] words = RemoveFromList(RemoveSpecial(RemoveHTML(p_Text.ToLower())), prohibitedWords).Split(' ');

            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word) && !prohibitedWords.Contains(word))
                {
                    if (_wordCounter.ContainsKey(word))
                    {
                        _wordCounter[word] = _wordCounter[word] + 1;
                    }
                    else
                    {
                        _wordCounter.Add(word, 1);
                    }

                    if (_tempCounter.ContainsKey(word))
                    {
                        _tempCounter[word] = _tempCounter[word] + 1;
                    }
                    else
                    {
                        _tempCounter.Add(word, 1);
                    }
                }
            }
        }

        private void LoadReservedString(List<string> prohibitedWords)
        {
            prohibitedWords.Add("\n");
            prohibitedWords.Add("\t");
        }

        private static void LoadPrepositions(List<string> prohibitedWords)
        {
            prohibitedWords.Add("a a");
            prohibitedWords.Add("à");
            prohibitedWords.Add("ao");
            prohibitedWords.Add("aos");
            prohibitedWords.Add("a");

            prohibitedWords.Add("de");
            prohibitedWords.Add("do");
            prohibitedWords.Add("dos");
            prohibitedWords.Add("dum");
            prohibitedWords.Add("duns");
            prohibitedWords.Add("da");
            prohibitedWords.Add("das");
            prohibitedWords.Add("duma");
            prohibitedWords.Add("dumas");

            prohibitedWords.Add("no");
            prohibitedWords.Add("para");
            prohibitedWords.Add("pra");
            prohibitedWords.Add("p'ra");
            prohibitedWords.Add("prá");
            prohibitedWords.Add("por");
            prohibitedWords.Add("em");
            prohibitedWords.Add("com");
            prohibitedWords.Add("disso");
            prohibitedWords.Add("desse");
            prohibitedWords.Add("deste");
            prohibitedWords.Add("dessa");
            prohibitedWords.Add("desta");
            prohibitedWords.Add("disto");
        }

        private void LoadArticles(List<string> prohibitedWords)
        {
            prohibitedWords.Add("a");
            prohibitedWords.Add("as");

            prohibitedWords.Add("o");
            prohibitedWords.Add("os");

            prohibitedWords.Add("ao");
            prohibitedWords.Add("aos");

            prohibitedWords.Add("à");
            prohibitedWords.Add("às");

            prohibitedWords.Add("da");
            prohibitedWords.Add("das");

            prohibitedWords.Add("do");
            prohibitedWords.Add("dos");

            prohibitedWords.Add("um");
            prohibitedWords.Add("uns");
            prohibitedWords.Add("dum");
            prohibitedWords.Add("duns");
            prohibitedWords.Add("num");
            prohibitedWords.Add("nuns");
            prohibitedWords.Add("uma");
            prohibitedWords.Add("umas");
            prohibitedWords.Add("duma");
            prohibitedWords.Add("dumas");
            prohibitedWords.Add("numa");
            prohibitedWords.Add("numas");
            prohibitedWords.Add("pelo");
            prohibitedWords.Add("pelos");
            prohibitedWords.Add("pela");
            prohibitedWords.Add("pelas");
        }

        private string RemoveFromList(string p_Text, List<string> values)
        {
            foreach (var item in values)
            {
                p_Text = p_Text.Replace(" " + item + " ", " ").Replace("  ", " ");
            }

            return p_Text;
        }

        private string RemoveHTML(string p_Text)
        {
            return Regex.Replace(p_Text, ".[^<]+[$>]", " ").Replace("  ", " ");
        }

        private string RemoveSpecial(string p_Text)
        {
            return Regex.Replace(p_Text, "[!?;.,-_(){}0-9]", " ").Replace("  ", " ");
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this._wordCounter != null)
                    {
                        this._wordCounter.Clear();
                        this._wordCounter = null;
                    }

                    if (this._tempCounter != null)
                    {
                        this._tempCounter.Clear();
                        this._tempCounter = null;
                    }
                }
                
                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
