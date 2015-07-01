using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace MaskedTextBoxBehavior
{
    [TypeConstraint(typeof(TextBox))]
    public sealed class MaskedTextBoxBehavior : DependencyObject, IBehavior
    {

        private MaskedTextProvider _Masker = new MaskedTextProvider();

        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }


        public TextBox AttachedTextBox { get { return AssociatedObject as TextBox; } }
        
        public string OriginalText { get; set; }

        public string Pattern
        {
            get
            {
                return (string)GetValue(PatternProperty);
            }

            set
            {
                SetValue(PatternProperty, value);
                _Masker.MatchPattern = value;
            }
        }

        public static DependencyProperty PatternProperty = DependencyProperty.Register("Pattern",
                                                                                       typeof(string),
                                                                                       typeof(MaskedTextBoxBehavior),
                                                                                       new PropertyMetadata(string.Empty));
        
        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            AttachedTextBox.TextChanged += AttachedTextBox_TextChanged;
            _Masker.MatchPattern = Pattern;
        }


        public void Detach()
        {
            AttachedTextBox.TextChanged -= AttachedTextBox_TextChanged;
        }


        void AttachedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var caret = AttachedTextBox.SelectionStart;
           
            AttachedTextBox.Text = _Masker.ReplaceString(AttachedTextBox.Text);
            AttachedTextBox.SelectionStart = caret + 1;
        }



    }

    public class MaskedTextProvider
    {
        public string RegExMatch { get; private set; }
        private List<int> _Whitespace;

        
        private string _MatchPattern;
        public string MatchPattern { get { return _MatchPattern; } set { _MatchPattern = value; ParseMatchMattern(); } }


        private Regex _Pattern;


        private void ParseMatchMattern()
        {
            StringBuilder regExMatch = new StringBuilder();
            _Whitespace = new List<int>();
            regExMatch.Append("^");

            for (int pos = 0; pos < MatchPattern.Length; pos++)
            {
                char symbol = MatchPattern[pos];
                switch (symbol)
                {
                    case '#': // Match Digit
                        regExMatch.Append(@"(\d{0,1})");
                        break;
                    default:
                        _Whitespace.Add(pos + 1);
                        regExMatch.Append(@"(\" + symbol + "{0,1})");
                        break;
                }
            }

            regExMatch.Append("$");

            RegExMatch = regExMatch.ToString();

            _Pattern = new Regex(RegExMatch);
        }

        public string ReplaceString(string enteredString)
        {
            var groups = _Pattern.Match(enteredString).Groups;
            var replaceString = new StringBuilder();

            for (int groupIndex = 1; groupIndex < groups.Count; groupIndex++)
            {
                var group = groups[groupIndex];

                if (group.Length > 0 && !_Whitespace.Contains(groupIndex))
                {
                    replaceString.Append("$" + groupIndex);
                } 
                else if (_Whitespace.Contains(groupIndex)) 
                {
                    // Peek to next group ...
                    if (groups.Count >= groupIndex + 1)
                    {
                        if (groups[groupIndex + 1].Length > 0)
                        {
                            replaceString.Append(_MatchPattern[groupIndex - 1]);                            
                        }
                        
                    }
                }
                else
                {
                    break; // stop if no more matches
                }
            }

            return _Pattern.Replace(enteredString, replaceString.ToString());
        }

    }
}
