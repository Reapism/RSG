using System.ComponentModel;

namespace RSG.Core.Models
{
    public class Search
    {
        [DisplayName("Search String")]
        public string SearchString { get; set; }

        [DisplayName("Character List")]
        public string CharacterList { get; set; }

        [DisplayName("Prediction Type")]
        public string PredictionType { get; set; }

        [DisplayName("Expected Character List")]
        public string ExpectedCharacterList { get; set; }
    }
}