using System;

namespace Wino.Mail.ViewModels
{
    public class Command
    {
        public void Execute(object value)
        {
            throw new NotImplementedException();
        }

        internal void NotifyCanExecuteChanged()
        {
            throw new NotImplementedException();
        }
    }
}