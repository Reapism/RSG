namespace RSG.Core.Exceptions
{
    public class PartitionIndexOutOfRange : RsgException
    {
        public PartitionIndexOutOfRange(int input, string parameterName)
            : base($"The parameter '{parameterName}' value {input} is out of range for the partition.")
        {
        }
    }
}
