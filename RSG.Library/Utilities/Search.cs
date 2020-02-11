using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Library.Utilities
{
    public class Search
    {
        private string _searchString;

        public BigInteger SearchIterations { get; set; }

        public static bool CanSearch { get; set; }

        public Search(string searchString)
        {
            _searchString = searchString;
            SearchIterations = BigInteger.Zero;
        }
        
        /// <summary>
        /// The original search algorithm.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="characterList"></param>
        /// <returns></returns>
        public bool OriginalSearch(ref string searchString, ref string characterList)
        {
            if (!CanSearch)
                return false;

            for (int i = 0; i < characterList.Length; i++)
            {
                if ((searchString + characterList[i]).Equals(_searchString))
                    return true;
                else
                {
                    SearchIterations += BigInteger.One;
                    if (searchString.Length + 1 < _searchString.Length)
                    {
                        var newSearchString = searchString + characterList[i];
                        if (OriginalSearch(ref newSearchString, ref characterList))
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
