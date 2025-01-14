using RSG.Services;

namespace RSG.Words
{
    public class WordListService
    {
        private readonly IRsgContext context;

        public WordListService(IRsgContext context)
        {
            this.context = context;
        }
        public WordList? FromInternalRequest(InternalWordListQuery request)
        {
            var words = context.Dictionaries.FirstOrDefault(e => e.ToInternalWordListRequest() == request);
            if (words is null)
            {
            }

            return words.WordList;
        }

        public static async Task<WordList?> FromExternalRequest(ExternalWordListRequest request)
        {
            switch (request.SourceType)
            {
                case SourceType.Web:
                {
                    return new WordList(await GetWordsFromWeb(request));
                }
                case SourceType.Filesystem:
                {
                    return new WordList(await GetWordsFromFileSystem(request));
                }
                default:
                {
                    return null;
                }
            }
        }

        public static async Task<Dictionary<int, string>> GetWordsFromWeb(ExternalWordListRequest request)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new UriBuilder(request.Source).Uri
            };

            var response = await httpClient.GetAsync(string.Empty);
            var content = await response.Content.ReadAsStringAsync();

            var hasContent = !string.IsNullOrEmpty(content);
            if (!hasContent)
            {
                return new Dictionary<int, string>();
            }
            var words = content
                       .Split(request.Delimiter)
                       .Select((e, i) => new { Key = i, Value = e })
                       .ToDictionary(e => e.Key, e => e.Value);

            return words;
        }

        public static async Task<Dictionary<int, string>> GetWordsFromFileSystem(ExternalWordListRequest request)
        {
            var fileStream = new FileStream(request.Source, FileMode.Open, FileAccess.Read, FileShare.Read);
            var streamReader = new StreamReader(fileStream);

            var words = (await streamReader.ReadToEndAsync())
                        .Split(request.Delimiter)
                        .Select((e, i) => new { Key = i, Value = e })
                        .ToDictionary(e => e.Key, e => e.Value);
            return words;
        }
    }
}
