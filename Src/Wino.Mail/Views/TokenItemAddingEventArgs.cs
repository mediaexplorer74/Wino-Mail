using System;
using Wino.Core.Domain.Entities;

namespace Wino.Views
{
    internal class TokenItemAddingEventArgs
    {
        internal string TokenText;
        internal bool Cancel;
        internal AddressInformation Item;

        public Defferal GetDeferral()
        {
            throw new NotImplementedException();
        }
    }
}