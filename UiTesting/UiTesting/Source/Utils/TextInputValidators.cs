using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }

    public interface ITextValidator
    {
        bool ValidateText(ref string text, string oldText);
    }

    public class TextValidatorNumbersOnly : ITextValidator
    {
        bool _allowDecimalPoint;
        double? _min;
        double? _max;

        public TextValidatorNumbersOnly(bool allowDecimal = false, double? min = null, double? max = null)
        {
            _allowDecimalPoint = allowDecimal;
            _min = min;
            _max = max;
        }

        public bool ValidateText(ref string text, string oldText)
        {
            if (text.Length == 0)
            {
                return true;
            }

            double num;

            if (_allowDecimalPoint)
            {
                if (!double.TryParse(text, out num))
                {
                    return false;
                }
            }
            else
            {
                int temp;
                if (!int.TryParse(text, out temp))
                {
                    return false;
                }
                num = temp;
            }

            if (_min != null && num < (double)_min) { text = _min.ToString(); }
            if (_max != null && num > (double)_max) { text = _max.ToString(); }

            return true;
        }
    }

    public class TextValidatorEnglishCharsOnly : ITextValidator
    {
        System.Text.RegularExpressions.Regex _regex;

        static System.Text.RegularExpressions.Regex _slugNoSpaces = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z|]+$");
            
        static System.Text.RegularExpressions.Regex _slugWithSpaces = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z|\ ]+$");

        public TextValidatorEnglishCharsOnly(bool allowSpaces = false)
        {
            _regex = allowSpaces ? _slugWithSpaces : _slugNoSpaces;
        }

        public bool ValidateText(ref string text, string oldText)
        {
            return (text.Length == 0 || _regex.IsMatch(text));
        }
    }

    public class SlugValidator : ITextValidator
    {
        System.Text.RegularExpressions.Regex _regex;

        static System.Text.RegularExpressions.Regex _slugNoSpaces = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z\-_]+$");

        static System.Text.RegularExpressions.Regex _slugWithSpaces = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z\-_\ ]+$");

        public SlugValidator(bool allowSpaces = false)
        {
            _regex = allowSpaces ? _slugWithSpaces : _slugNoSpaces;
        }

        public bool ValidateText(ref string text, string oldText)
        {
            return (text.Length == 0 || _regex.IsMatch(text));
        }
    }

    public class OnlySingleSpaces : ITextValidator
    {
           
        public bool ValidateText(ref string text, string oldText)
        {
            return !text.Contains("  ") && !text.Contains("\t");
        }
    }

    public class TextValidatorMakeTitle : ITextValidator
    {
        public bool ValidateText(ref string text, string oldText)
        {
            if (text.Length > 0)
            {
                text = text.ToLower();
                text = text[0].ToString().ToUpper() + text.Remove(0, 1);
            }
            return true;
        }
    }
}

