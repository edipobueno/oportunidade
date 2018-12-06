using System;
using System.Collections.Generic;
using System.Text;

namespace console.Models
{
    [Serializable]
    public class FeedModel
    {
        public string title { get; set; }
        public int wordsCount { get; set; }
    }
}
