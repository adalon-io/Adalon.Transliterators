namespace Adalon.Globalization.Transliterators
{
    public interface ITransliterator
    {
        TargetLanguage TargetLanguage { get; }
        string Transliterate(string original);

    }
}
