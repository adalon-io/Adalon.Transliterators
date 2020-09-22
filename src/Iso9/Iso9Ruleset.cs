using System;
using System.Collections.Generic;
using Adalon.Globalization.Transliterators.Internal;

namespace Adalon.Globalization.Transliterators
{
    internal static class Iso9Ruleset
    {
        private static readonly Lazy<Dictionary<char, FrugalLocalList<Iso9Rule>>> RulesLazy =
            new Lazy<Dictionary<char, FrugalLocalList<Iso9Rule>>>(BuildRules);

        private static Dictionary<char, FrugalLocalList<Iso9Rule>> GetRules() => RulesLazy.Value;


        public static bool HasRule(char ch)
        {
            var rules = GetRules();
            return rules.ContainsKey(ch);
        }

        public static FrugalLocalList<Iso9Rule> RulesFor(char ch)
        {
            var rules = GetRules();
            return rules[ch];
        }

        private static Dictionary<char, FrugalLocalList<Iso9Rule>> BuildRules()
        {
            // simple
            var regular = new Char3[]
            {
                "А", "A", "Ӓ", "Ä", "Ӓ̄", "Ạ̈", "Ӑ", "Ă", "А̄", "Ā", "Ӕ", "Æ", "А́", "Á", "А̊", "Å", "Б", "B", "В", "V",
                "Г", "G", "Ѓ", "Ǵ", "Ғ", "Ġ", "Ҕ", "Ğ", "Һ", "Ḥ", "Д", "D", "Ђ", "Đ", "Е", "E", "Ӗ", "Ĕ", "Ё", "Ë",
                "Є", "Ê", "Ж", "Ž", "Ӝ", "Z̄", "Ӂ", "Z̆", "З", "Z", "Ӟ", "Z̈", "Ӡ", "Ź", "Ѕ", "Ẑ", "И", "I", "Ӣ", "Ī",
                "И́", "Í", "Ӥ", "Î", "Й", "J", "І", "Ì", "Ї", "Ï", "І̄", "Ǐ", "Ј", "J̌", "Ј̵", "J́", "К", "K", "Ќ", "Ḱ",
                "Ӄ", "Ḳ", "Ҝ", "K̂", "Ҡ", "Ǩ", "Ҟ", "K̄", "К̨", "K̀", "Ԛ", "Q", "Л", "L", "Љ", "L̂", "М", "M", "Н", "N",
                "Њ", "N̂", "Ӊ", "Ṇ", "Ҥ", "Ṅ", "Ԋ", "Ǹ", "Ԣ", "Ń", "Ӈ", "Ň", "Н̄", "N̄", "О", "O", "Ӧ", "Ö", "Ө", "Ô",
                "Ӫ", "Ő", "Ӧ̄", "Ọ̈", "Ҩ", "Ò", "О́", "Ó", "О̄", "Ō", "Œ", "Œ", "П", "P", "Ҧ", "Ṕ", "Ԥ", "P̀", "Р", "R",
                "С", "S", "С̀", "S̀", "Т", "T", "Ћ", "Ć", "Ԏ", "T̀", "Т̌", "Ť", "У", "U", "Ӱ", "Ü", "Ӯ", "Ū", "Ў", "Ŭ",
                "Ӳ", "Ű", "У́", "Ú", "Ү", "Ù", "Ұ", "U̇", "Ԝ", "W", "Ф", "F", "Х", "H", "Ц", "C", "Ҵ", "C̄", "Џ", "D̂",
                "Ч", "Č", "Ӌ", "C̣", "Ӵ", "C̈", "Ҹ", "Ĉ", "Ч̀", "C̀", "Ҽ", "C̆", "Ҿ", "C̨̆", "Ш", "Š", "Щ", "Ŝ", "Ъ",
                "ʺ",
                "Ы", "Y", "Ӹ", "Ÿ", "Ы̄", "Ȳ", "Ь", "ʹ", "Э", "È", "Ә", "A̋", "Ӛ", "À", "Ю", "Û", "Ю̄", "Û̄", "Я", "Â",
                "Ґ", "G̀", "Ѣ", "Ě", "Ѫ", "Ǎ", "Ѳ", "F̀", "Ѵ", "Ỳ", "а", "a", "ӓ", "ä", "ӓ̄", "ạ̈", "ӑ", "ă", "а̄", "ā",
                "ӕ", "æ", "а́", "á", "а̊", "å", "б", "b", "в", "v", "г", "g", "ѓ", "ǵ", "ғ", "ġ", "ҕ", "ğ", "һ", "ḥ",
                "д", "d", "ђ", "đ", "е", "e", "ӗ", "ĕ", "ё", "ë", "є", "ê", "ж", "ž", "ӝ", "z̄", "ӂ", "z̆", "з", "z",
                "ӟ", "z̈", "ӡ", "ź", "ѕ", "ẑ", "и", "i", "ӣ", "ī", "и́", "í", "ӥ", "î", "й", "j", "і", "ì", "ї", "ï",
                "і̄", "ǐ", "ј", "ǰ", "ј̵", "j́", "к", "k", "ќ", "ḱ", "ӄ", "ḳ", "ҝ", "k̂", "ҡ", "ǩ", "ҟ", "k̄", "к̨",
                "k̀",
                "ԛ", "q", "л", "l", "љ", "l̂", "м", "m", "н", "n", "њ", "n̂", "ӊ", "ṇ", "ҥ", "ṅ", "ԋ", "ǹ", "ԣ", "ń",
                "ӈ", "ň", "н̄", "n̄", "о", "o", "ӧ", "ö", "ө", "ô", "ӫ", "ő", "о̄̈", "ọ̈", "ҩ", "ò", "о́", "ó", "о̄",
                "ō",
                "œ", "œ", "п", "p", "ҧ", "ṕ", "ԥ", "p̀", "р", "r", "с", "s", "с̀", "s̀", "т", "t", "ћ", "ć", "ԏ", "t̀",
                "т̌", "ť", "у", "u", "ӱ", "ü", "ӯ", "ū", "ў", "ŭ", "ӳ", "ű", "у́", "ú", "ү", "ù", "ұ", "u̇", "ԝ", "w",
                "ф", "f", "х", "h", "ц", "c", "ҵ", "c̄", "џ", "d̂", "ч", "č", "ӌ", "c̣", "ӵ", "c̈", "ҹ", "ĉ", "ч̀",
                "c̀",
                "ҽ", "c̆", "ҿ", "c̨̆", "ш", "š", "щ", "ŝ", "ъ", "ʺ", "ы", "y", "ӹ", "ÿ", "ы̄", "ȳ", "ь", "ʹ", "э", "è",
                "ә", "a̋", "ӛ", "à", "ю", "û", "ю̄", "û̄", "я", "â", "ґ", "g̀", "ѣ", "ě", "ѫ", "ǎ", "ѳ", "f̀", "ѵ", "ỳ",
                "Ӏ", "‡", "ʼ", "`", "ˮ", "¨"
            };
            var withDescender = new Char3[]
            {
                "Җ", "Ž̦", "Ž̧", "Қ", "K̦", "Ķ", "Ԡ", "L̦", "Ļ", "Ң", "N̦", "Ņ", "Ҫ", "Ș", "Ş", "Ҭ", "Ț", "Ţ", "Ҳ",
                "H̦",
                "Ḩ", "Ҷ", "C̦", "Ç", "җ", "ž̦", "ž̧", "қ", "k̦", "ķ", "ԡ", "l̦", "ļ", "ң", "n̦", "ņ", "ҫ", "ș", "ş",
                "ҭ",
                "ț", "ţ", "ҳ", "h̦", "ḩ", "ҷ", "c̦", "ç"
            };
            var withUmlautAndMacron = new Char3[]
            {
                "Ӱ̄", "Ụ̈", "Ụ̄", "ӱ̄", "ụ̈", "ụ̄"
            };
            var result = new Dictionary<char, FrugalLocalList<Iso9Rule>>();
            for (int i = 0; i < regular.Length; i += 2)
            {
                var rule = new Iso9Rule(regular[i], regular[i + 1], default, false, false);
                if (!result.ContainsKey(rule.From.C0)) result[rule.From.C0] = new FrugalLocalList<Iso9Rule>();
                var lst = result[rule.From.C0];
                lst.Add(rule);
                result[rule.From.C0] = lst;
            }

            for (int i = 0; i < withDescender.Length; i += 3)
            {
                var rule = new Iso9Rule(withDescender[i], withDescender[i + 1], withDescender[i + 2], true,
                    false);
                if (!result.ContainsKey(rule.From.C0)) result[rule.From.C0] = new FrugalLocalList<Iso9Rule>();
                var lst = result[rule.From.C0];
                lst.Add(rule);
                result[rule.From.C0] = lst;
            }

            for (int i = 0; i < withUmlautAndMacron.Length; i += 3)
            {
                var rule = new Iso9Rule(withUmlautAndMacron[i], withUmlautAndMacron[i + 1], withUmlautAndMacron[i + 2],
                    false, true);
                if (!result.ContainsKey(rule.From.C0)) result[rule.From.C0] = new FrugalLocalList<Iso9Rule>();
                var lst = result[rule.From.C0];
                lst.Add(rule);
                result[rule.From.C0] = lst;
            }

            foreach (var list in result.Values)
            {
                if (list.Count > 1)
                {
                    list.ListRef.Sort(Char3Iso9RuleSequenceComparer.Instance);
                }
            }

            return result;
        }


        internal readonly struct Iso9Rule
        {
            public Iso9Rule(Char3 from, Char3 to, Char3 alt, bool commaOrCedilla, bool macronOrDiaresis)
            {
                From = from;
                To = to;
                Alternative = alt;
                IsCharacterWithDescender = commaOrCedilla;
                IsCharacterWithUmlautAndMacron = macronOrDiaresis;
            }

            public Char3 From { get; }
            public Char3 To { get; }
            public Char3 Alternative { get; }
            public bool IsCharacterWithDescender { get; }
            public bool IsCharacterWithUmlautAndMacron { get; }
        }

        private class Char3Iso9RuleSequenceComparer : IComparer<Iso9Rule>
        {
            public static readonly Char3Iso9RuleSequenceComparer Instance = new Char3Iso9RuleSequenceComparer();

            public int Compare(Iso9Rule x, Iso9Rule y)
            {
                var xfrom = x.From;
                var yfrom = y.From;
                if (xfrom.C0 != yfrom.C0) return xfrom.C0.CompareTo(yfrom.C0);
                return yfrom.Lenth.CompareTo(xfrom.Lenth);
            }
        }
    }
}