using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Utilities
{
    public class RandomWordGenerator
    {
        private readonly DictionaryService dictionaryService;
        private RsgDictionary dictionary;
        private IDictionary<int, string> wordList;
        private int min;
        private int max;

        public RandomWordGenerator(DictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
            InstantiateInstance();
        }

        private void InstantiateInstance()
        {
            dictionary = dictionaryService.GetSelectedDictionary();
            wordList = dictionary.WordList;
            min = 0;
            max = dictionary.WordList.Count();
        }

        public IDictionaryResult GenerateRandomWords(int numberOfIterations)
        {
            return GenerateRandomWords(numberOfIterations.ToBigInteger());
        }

        public IDictionaryResult GenerateRandomWords(BigInteger numberOfIterations)
        {
            var startTime = DateTime.Now;
            var words = new Queue<string>();

            for (var bi = BigInteger.Zero; bi < numberOfIterations; bi++)
            {
                words.Enqueue(GenerateRandomWord());
            }

            var endTime = DateTime.Now;
            var result = new DictionaryResult()
            {
                Dictionary = dictionary,
                Iterations = numberOfIterations,
                RandomizationType = RandomProvider.SelectedRandomizationType,
                Words = default

                // Need to externalize the AddWords logic in Words struct.
                // Maybe create more methods in dictionary service that do this.
                // RandomWordGenerator should be able to create the Words struct
                // with generated words.
            };
            return result;

        }

        private string GenerateRandomWord()
        {
            var rndValue = RandomProvider.Random.Next(min, max);

            return dictionary.WordList[rndValue];
        }

        private Tuple<BigInteger, BigInteger> GetNumberOfPartitionsAndLastPartition(in BigInteger numberOfWords)
        {
            BigInteger numberOfPartitions = BigInteger.DivRem(numberOfWords, BigInteger.Parse(PartitionSize.ToString()), out BigInteger remainder);
            Tuple<BigInteger, BigInteger> lastPartition = Tuple.Create(numberOfPartitions, remainder);

            return lastPartition;
        }

        private IEnumerable<Thread> GetThreads(in BigInteger numberOfWords, ThreadPriority threadPriority)
        {
            Queue<Thread> queue = new Queue<Thread>();
            Tuple<BigInteger, BigInteger> partitions = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            BigInteger fullPartitions = partitions.Item1 - BigInteger.One;

            for (BigInteger currentPartition = BigInteger.Zero; currentPartition < fullPartitions;)
            {
                queue.Enqueue(
                    new Thread(
                        new ThreadStart(PopulateWords))
                    {
                        IsBackground = true,
                        Priority = threadPriority,
                        Name = $"Words_PartitionIndex_{currentPartition}",
                    });
            }

            queue.Enqueue(new Thread(new ThreadStart(PopulatePartialWords))
            {
                IsBackground = true,
                Priority = threadPriority,
                Name = $"Words_PartitionIndex_{fullPartitions}",
            });

            return queue;
        }

        private void ExecuteThreads(IEnumerable<Thread> threads, in BigInteger lastPartition)
        {
            int fullPartitionedThreads = threads.Count() - 1;

            for (int i = 0; i < fullPartitionedThreads; i++, threads.GetEnumerator().MoveNext())
            {
                Thread thread = threads.GetEnumerator().Current;
                thread.Start();
            }

            // start last thread with parameter
            threads.GetEnumerator().Current.Start(lastPartition);
        }

        private void PopulateWords()
        {

        }

        private void PopulatePartialWords()
        {

        }
    }
}
