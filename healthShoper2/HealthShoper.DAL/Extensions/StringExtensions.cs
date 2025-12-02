using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace HealthShoper.DAL.Extensions;

public static class StringExtensions
{
    public static string HashSecretKey(this string secretKey)
    {
        var message = Encoding.UTF8.GetBytes(secretKey);
        using var sha1 = SHA512.Create();

        var hashValue = sha1.ComputeHash(message);
        return Convert.ToBase64String(hashValue);
    }
    
    /// <summary>
    /// Substring с коррекцией границ
    /// startIndex имеет приоритет перед length
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string SmartSubstring(this string txt, int startIndex, int length)
    {
        if (txt == null)
        {
            return null;
        }

        if (startIndex < 0)
        {
            startIndex = 0;
        }

        if (startIndex > txt.Length)
        {
            startIndex = txt.Length;
        }

        if (length > (txt.Length - startIndex))
        {
            length = (txt.Length - startIndex);
        }

        if (length < 0)
        {
            length = 0;
        }

        return txt.Substring(startIndex, length);
    }
    
    /// <summary>
    /// Возвращает строку, в которой первая буква делается заглавной
    /// </summary>
    public static string FirstCharToUpper(this string txt)
    {
        if (string.IsNullOrEmpty(txt) || char.IsUpper(txt, 0))
        {
            return txt;
        }

        return $"{char.ToUpper(txt[0])}{txt[1..]}";
    }
    
    /// <summary>
    /// Разделяет большое слово на части по максимум (30) знаков каждая
    /// </summary>
    public static IEnumerable<string> SplitLongWord(this string text, int maxWordLength)
    {
        if (text.Length <= maxWordLength)
        {
            yield return text;
        }
        else
        {
            for (var i = 0; i < text.Length; i += maxWordLength)
            {
                yield return text.Substring(i, Math.Min(maxWordLength, text.Length - i));
            }
        }
    }
    
    public static bool HasValue(this string str)
    {
        return !str.NotHasValue();
    }

    public static bool NotHasValue(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    
    private static CaseMode getCaseMode(string txt)
    {
        var firstLetterUpper = char.IsUpper(txt[0]);
        var allOtherLettersLow = true;
        for (var i = 1; i < txt.Length; ++i)
        {
            if (char.IsUpper(txt[i]))
            {
                allOtherLettersLow = false;
                break;
            }
        }

        if (firstLetterUpper && allOtherLettersLow)
        {
            return CaseMode.OnlyFirstCharUpperCase;
        }

        if (!firstLetterUpper && allOtherLettersLow)
        {
            return CaseMode.AllCharsLowCase;
        }

        return CaseMode.OtherCase;
    }

    public static bool EqualsAny(
        this string compareThat,
        StringComparison comparisonType,
        params string[] strings
    )
    {
        return EqualsAny(compareThat, comparisonType, (IEnumerable<string>)strings);
    }

    public static bool EqualsAny(
        this string compareThat,
        StringComparison comparisonType,
        IEnumerable<string> strings
    )
    {
        return strings.Any(compareWith => string.Equals(compareThat, compareWith, comparisonType));
    }
    
    public static string ToCamelCase(this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        if (str.Length == 1)
        {
            return str.ToLower(CultureInfo.InvariantCulture);
        }

        return char.ToLowerInvariant(str[0]) + str[1..];
    }

    // string to PascalCase
    public static string ToPascalCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        // Replace all non-letter and non-digits with an underscore and lowercase the rest.
        var sample = string.Join(
            string.Empty,
            str.Select(c =>
                char.IsLetterOrDigit(c) ? c.ToString().ToLower(CultureInfo.InvariantCulture) : "_"
            )
        );

        // Split the resulting string by underscore
        // Select first character, uppercase it and concatenate with the rest of the string
        var arr = sample
            .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => $"{s[..1].ToUpper(CultureInfo.InvariantCulture)}{s[1..]}");

        // Join the resulting collection
        sample = string.Join(string.Empty, arr);

        return sample;
    }
    
    #region nested types

    private enum CaseMode
    {
        AllCharsLowCase,
        OnlyFirstCharUpperCase,
        OtherCase
    }

    #endregion

}