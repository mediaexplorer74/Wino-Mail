using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Wino.Views
{
    public class TokenizingTextBox
    {
        public string Tag;
        public List<ITokenStringContainer> Items;
        internal string Text;

        public EventHandler<AutoSuggestBoxTextChangedEventArgs> TextChanged { get;  set; }
    }
}