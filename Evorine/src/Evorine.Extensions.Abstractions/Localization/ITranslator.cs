using System.Collections.Generic;

namespace Evorine.Localization.Abstractions
{
    public interface ITranslator
    {
        string Translate(string text);

        string Translate(string text, params object[] args);

        string Translate(string text, IDictionary<string, string> dictionary);
    }
}