using RSG.Core.Interfaces;
using System;
using System.ComponentModel;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Event handler for when the <see cref="RandomWordGenerator.GenerateRandomWordsResult(System.Numerics.BigInteger)"/>
    /// InProgress event is fired.
    /// </summary>
    /// <param name="sender">The object who fired the event.</param>
    /// <param name="e">The <see cref="ProgressChangedEventArgs"/>.</param>
    public delegate void GenerateRandomWordsResultProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);

    /// <summary>
    /// Event handler for when the <see cref="RandomWordGenerator.GenerateRandomWordsResult(System.Numerics.BigInteger)"/>
    /// Completed event is fired.
    /// </summary>
    /// <param name="sender">The objhect who fired the event.</param>
    /// <param name="e">The <see cref="GenerateRandomWordsResultEventArgs"/> args.</param>
    public delegate void GenerateRandomWordsResultCompletedEventHandler(object sender, GenerateRandomWordsResultEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class GenerateRandomWordsResultEventArgs : AsyncCompletedEventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateRandomWordsResultEventArgs"/> class with a
        /// <see cref="IDictionaryResult"/> instance.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isCancelled"></param>
        /// <param name="errorState"></param>
        /// <param name="result"></param>
        public GenerateRandomWordsResultEventArgs(Exception e, bool isCancelled, object errorState, IDictionaryResult result)
            : base(e, isCancelled, errorState)
        {
            Result = result;
        }

        /// <summary>
        /// Gets the result of the <see cref="RandomWordGenerator.GenerateRandomWordsResult(System.Numerics.BigInteger)"/>
        /// member function.
        /// </summary>
        public IDictionaryResult Result { get; }
    }
}
