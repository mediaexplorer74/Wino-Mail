using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Wino.Core.Domain.Models.Comparers;
using Wino.Core.Domain.Models.MailItem;
using Wino.Mail.ViewModels.Data;

namespace Wino.Mail.ViewModels.Collections
{
    public class ObservableGroupedCollection<T1, T2>
    {
        public int Count;

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public SomeElement ElementAt(int i)
        {
            throw new NotImplementedException();
        }

        public void InsertItem(object groupKey, ListItemComparer listComparer, MailItemViewModel mailItemViewModel, IComparer<IMailItem> comparer)
        {
            throw new NotImplementedException();
        }

        public void InsertItem(object groupKey, ListItemComparer listComparer, ThreadMailItemViewModel threadMailItemViewModel, IComparer<IMailItem> comparer)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(object key)
        {
            throw new NotImplementedException();
        }

       

        public void AddGroup(object key, ObservableGroup<object, IMailItem> group)
        {
            throw new NotImplementedException();
        }

        public Group FirstGroupByKeyOrDefault(object key)
        {
            throw new NotImplementedException();
        }
    }
}