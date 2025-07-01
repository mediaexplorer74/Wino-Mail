using System;

namespace Wino.Core.MenuItems
{
    internal class NotifyPropertyChangedForAttribute : Attribute
    {
        private string v;

        public NotifyPropertyChangedForAttribute(string v)
        {
            this.v = v;
        }
    }
}