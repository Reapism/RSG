using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.View.Views
{
    public class StringViewModel
    {
        private IList<RandomizationType> randomizationTypes = new List<RandomizationType>()
        {
            RandomizationType.Pseudorandom,
            RandomizationType.ReapRandom
        };

        public RandomizationType SelectedRandomizationType { get; set; }

        public BigInteger StringLength { get; set; }

        public BigInteger Iterations { get; set; }

        public IEnumerable<RandomizationType> RandomizationTypes { get => randomizationTypes; }

        public char[] CharacterList { get; set; }
        public string Test { get; set; }
    }
}
