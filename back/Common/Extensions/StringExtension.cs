using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Exceptions;

namespace Common.Extensions;

public static class StringExtension
{
    private static Dictionary<char, string> LatinSymDict { get; } = new()
    {
        { 'а', "a" },
        { 'б', "b" },
        { 'в', "v" },
        { 'г', "g" },
        { 'д', "d" },
        { 'е', "e" },
        { 'ё', "yo" },
        { 'ж', "zh" },
        { 'з', "z" },
        { 'и', "i" },
        { 'й', "j" },
        { 'к', "k" },
        { 'л', "l" },
        { 'м', "m" },
        { 'н', "n" },
        { 'о', "o" },
        { 'п', "p" },
        { 'р', "r" },
        { 'с', "s" },
        { 'т', "t" },
        { 'у', "u" },
        { 'ф', "f" },
        { 'х', "kh" },
        { 'ц', "ts" },
        { 'ч', "ch" },
        { 'ш', "sh" },
        { 'щ', "shch" },
        { 'ь', "" },
        { 'ы', "y" },
        { 'ъ', "" },
        { 'э', "e" },
        { 'ю', "yu" },
        { 'я', "ya" },
        { 'ң', "n" },
        { 'ө', "o" },
        { 'ү', "u" }
    };

    public static string ToLatin(this string word)
    {
        var lst = word.Select(x =>
        {
            var lowX = x.ToLower();
            var latX = LatinSymDict.ContainsKey(lowX) ? LatinSymDict[lowX] : lowX.ToString();
            return x.IsUpper() ? latX.Capitalize() : latX;
        });

        return string.Join(null, lst);
    }

    public static string Capitalize(this string str)
    {
        if (string.IsNullOrEmpty(str))
            throw new InnerException($"2520. Пустая строка.");

        return $"{str.Substring(0, 1).ToUpper()}{str.Substring(1).ToLower()}";
    }

    public static string ToWithoutWhitespaces(this string str)
    {
        return Regex.Replace(str, @"\s+", string.Empty);
    }

    public static string Slice(this string str, int len)
    {
        return str.Length > len
            ? str.Substring(0, len)
            : str;
    }

    public static string CutExceptionCode(this string str)
    {
        return str.ByRegex(new Regex(@"^(\d+)\.\s?(.+?)$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant), 2);
    }

    private static string ByRegex(this string str, Regex re, int groupIndex)
    {
        var match = re.Match(str);
        return match.Success ? match.Groups[groupIndex].Value : str;
    }

    public static int? ExceptionCode(this string str)
    {
        var re = new Regex(@"^(\d+)\.\s?(.+?)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        var match = re.Match(str);

        return match.Success
            ? match.Groups[1].Value.ToInt($"2556.")
            : null;
    }

    public static string WrapWithSquareBrackets(this string str)
    {
        return str.WrapWithDiffWords("[", "]");
    }

    public static string WrapWithSameWords(this string str, string word)
    {
        return str.WrapWithDiffWords(word, word);
    }

    public static string WrapWithDiffWords(this string str, string left, string right)
    {
        return $"{left}{str}{right}";
    }

    public static bool IsBase64Str(this string str)
    {
        var buffer = new byte[(str.Length * 3 + 3) / 4 - (str.Length > 0 && str[^1] == '='
            ? str.Length > 1 && str[^2] == '='
                ? 2 : 1 : 0)];

        return Convert.TryFromBase64String(str, buffer, out _);
    }

    public static bool IsNotNullOrWhiteSpace(this string str)
    {
        return !str.IsNullOrWhiteSpace();
    }

    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static string Format(this string str, params string[] lst)
    {
        return string.Format(str, lst);
    }

    public static string Join(this string[] lst)
    {
        return string.Join(", ", lst);
    }

    public static string Join(this IEnumerable<string> lst)
    {
        return string.Join(", ", lst);
    }
}
