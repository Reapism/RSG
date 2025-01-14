using System.Numerics;

namespace RSG.Words
{
    public record WordList(Dictionary<int, string> Words);
    public record WordRequest(int Iterations, IList<InternalWordListQuery> WordListRequests);
    public record WordStatistics(BigInteger WordCount, int SizeInNumberOfBytes);
    public record WordResult(WordRequest Request, Dictionary<int, Dictionary<int, string>> Words);

    public record InternalWordListQuery(Guid? Id, string? Name);

    public enum SourceType
    {
        Web,
        Filesystem
    }

    /// <summary>
    /// A mechanism for retrieving a word list by searching externally outside this context.
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="SourceType"></param>
    /// <param name="WordListName"></param>
    /// <param name="WordListDescription"></param>
    /// <param name="Delimiter"></param>
    public record ExternalWordListRequest(string Source, SourceType SourceType, string WordListName, string WordListDescription, string Delimiter);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Statistics"></param>    /// <param name="Id"></param>
    /// <param name="Name"> Gets the name of the dictionary. </param>
    /// <param name="Description"> Gets the description of the dictionary. </param>
    /// <param name="IsActive"> Gets a value indicating whether this dictionary is active or not. </param>
    /// <param name="Options"> Gets a value indicating the options of the word list. </param>
    /// <param name="Statistics"></param>
    public record WordDictionary(Guid Id, string Name, string Description, bool IsActive, WordList WordList, WordOptions Options, WordStatistics Statistics)
    {
        public InternalWordListQuery ToInternalWordListRequest()
        {
            return new InternalWordListQuery(Id, Name);
        }
    }

    /// <param name="Source"> Gets the source path.
    /// <para>Can be a local file path or from a web source.</para> </param>
    /// <param name="Delimiter"> Gets or sets the delimiter used to seperate words from other words in the word list source. </param>
    public record WordOptions(string Source, string Delimiter)
    {
        public bool IsLocalSource()
        {
            if (Source.StartsWith("http") || Source.StartsWith("www"))
            {
                return false;
            }

            if (!Path.Exists(Source))
            {
                return false;
            }

            return true;
        }
    }
}
 