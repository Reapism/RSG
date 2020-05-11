namespace RSG.Core.Interfaces.Configuration
{
    public interface IDictionaryConfiguration
    {
        IRsgDictionary Dictionary { get; set; }

        bool UseSpace { get; set; }

        bool CapitalizeEachWord { get; set; }

        char AliterationCharacter { get; set; }


    }
}
