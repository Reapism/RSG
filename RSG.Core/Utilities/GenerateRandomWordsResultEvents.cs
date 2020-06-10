using RSG.Core.Interfaces;
using System;
using System.ComponentModel;

namespace RSG.Core.Utilities
{
    public delegate void GenerateRandomWordsResultProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);

    public delegate void GenerateRandomWordsResultCompletedEventHandler(object sender, GenerateRandomWordsResultEvents e);

    public class GenerateRandomWordsResultEvents : AsyncCompletedEventArgs
    {
        public GenerateRandomWordsResultEvents(Exception e, bool isCancelled, object errorState, IDictionaryResult result)
            : base(e, isCancelled, errorState)
        {
            Result = result;
        }

        public IDictionaryResult Result { get; }
    }
}
