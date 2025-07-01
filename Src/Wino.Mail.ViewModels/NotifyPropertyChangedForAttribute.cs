using System;

namespace Wino.Mail.ViewModels
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