using System;

namespace Wino.Mail.ViewModels
{
    internal class RelayCommandAttribute : Attribute
    {
        public string CanExecute;
    }
}