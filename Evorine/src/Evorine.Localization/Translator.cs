using Evorine.Localization.Abstractions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization
{
    public class Translator : ITranslator
    {
        readonly ITranslatorCore translatorCore;
        readonly string module;
        readonly CultureInfo culture;

        readonly IDictionary<string, string> dictionary;

        public string Module
        {
            get
            {
                return module;
            }
        }

        public CultureInfo Culture
        {
            get
            {
                return culture;
            }
        }
        

        public Translator(ITranslatorCore translatorCore, CultureInfo culture, string module)
        {
            dictionary = new ConcurrentDictionary<string, string>();
            this.translatorCore = translatorCore;
            this.culture = culture;
            this.module = module;
        }
        
        public string Translate(string text)
        {
            return translatorCore.Translate(module, culture, text);
#if DEBUG
            if (dictionary.ContainsKey(text)) return dictionary[text];

            Task.Run(() => {
                var translatedText = translatorCore.Translate(module, culture, text);

                if (dictionary.ContainsKey(text)) return;
                dictionary[text] = translatedText;
            });
            return text;
#else
            if (dictionary.ContainsKey(text)) return dictionary[text];

            var translatedText = translatorCore.Translate(module, culture, text);
            dictionary[text] = translatedText;
            return translatedText;
#endif
        }

        public string Translate(string text, IDictionary<string, string> dictionary)
        {
            text = Translate(text);
            foreach (var element in dictionary)
            {
                text = text.Replace("{" + element.Key + "}", element.Value);
            }
            return text;
        }

        public string Translate(string text, params object[] args)
        {
            text = Translate(text);
            if (args.Last() is System.Collections.IDictionary)
            {
                foreach (var node in args.Last() as IDictionary<string, string>)
                {
                    text.Replace("{" + node.Key + "}", node.Value);
                }
                args = args.Take(args.Length - 1).ToArray();
            }
            return string.Format(text, args);
        }
    }
}