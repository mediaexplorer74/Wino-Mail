using System;
using Wino.Core.Domain.Models.MailItem;
using Wino.Mail.ViewModels.Data;

namespace Wino.Mail.ViewModels.Collections
{
    internal class Group
    {
        internal int Count;

        internal void Add(IMailItem item)
        {
            throw new NotImplementedException();
        }

        internal MailItemViewModel ElementAt(int k)
        {
            throw new NotImplementedException();
        }
    }
}