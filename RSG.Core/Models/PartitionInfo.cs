using System.ComponentModel.Design.Serialization;
using System.Numerics;

namespace RSG.Core.Models
{
    internal class PartitionInfo
    {
        internal BigInteger TotalIterations { get; set; }

        internal int NumberOfPartitions { get; set; }

        internal int FullPartitionSize { get; set; }

        internal int LastPartitionSize { get; set; }

        internal int CurrentIndex { get; set; }

        public int GetPartitionSize()
        {
            if (IsLastPartition(CurrentIndex))
            {
                return LastPartitionSize;
            }

            return FullPartitionSize;
        }

        private bool IsLastPartition(int currentIndex)
        {
            if (currentIndex < NumberOfPartitions - 1)
            {
                return false;
            }

            return true;
        }
    }
}
