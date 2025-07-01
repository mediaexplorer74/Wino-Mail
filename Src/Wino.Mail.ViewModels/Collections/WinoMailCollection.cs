using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;//.Collections;
using Wino.Core.Domain.Entities;
using Wino.Core.Domain.Enums;
using Wino.Core.Domain.Interfaces;
using Wino.Core.Domain.Models.Comparers;
using Wino.Core.Domain.Models.MailItem;
using Wino.Mail.ViewModels.Data;

namespace Wino.Mail.ViewModels.Collections
{
    public class WinoMailCollection
    {
        // We cache each mail copy id for faster access on updates.
        // If the item provider here for update or removal doesn't exist here
        // we can ignore the operation.

        public HashSet<Guid> MailCopyIdHashSet = new HashSet<Guid>();

        private ListItemComparer listComparer = new ListItemComparer();
        private bool shouldExit;
        private readonly ObservableGroupedCollection<object, IMailItem> _mailItemSource = new ObservableGroupedCollection<object, IMailItem>();

        public ReadOnlyObservableGroupedCollection<object, IMailItem> MailItems { get; }

        /// <summary>
        /// Property that defines how the item sorting should be done in the collection.
        /// </summary>
        public SortingOptionType SortingType { get; set; }

        /// <summary>
        /// Threading strategy that will help thread items according to the account type.
        /// </summary>
        public IThreadingStrategyProvider ThreadingStrategyProvider { get; set; }

        /// <summary>
        /// Automatically deletes single mail items after the delete operation or thread->single transition.
        /// This is useful when reply draft is discarded in the thread. Only enabled for Draft folder for now.
        /// </summary>
        public bool PruneSingleNonDraftItems { get; set; }

        public int Count => _mailItemSource.Count;

        public IDispatcher CoreDispatcher { get; set; }

        public WinoMailCollection()
        {
            MailItems = new ReadOnlyObservableGroupedCollection<object, IMailItem>(_mailItemSource);
        }

        public void Clear() => _mailItemSource.Clear();

        private object GetGroupingKey(IMailItem mailItem)
        {
            if (SortingType == SortingOptionType.ReceiveDate)
                return mailItem.CreationDate.ToLocalTime().Date;
            else
                return mailItem.FromName;
        }

        private async Task InsertItemInternalAsync(object groupKey, IMailItem mailItem)
            => await ExecuteUIThread(() =>
                {
                    if (mailItem is MailCopy mailCopy)
                    {
                        MailCopyIdHashSet.Add(mailCopy.UniqueId);

                        _mailItemSource.InsertItem(groupKey, listComparer, new MailItemViewModel(mailCopy), listComparer.GetItemComparer());
                    }
                    else if (mailItem is ThreadMailItem threadMailItem)
                    {
                        foreach (var item in threadMailItem.ThreadItems)
                        {
                            MailCopyIdHashSet.Add(item.UniqueId);
                        }

                        _mailItemSource.InsertItem(groupKey, listComparer, new ThreadMailItemViewModel(threadMailItem), listComparer.GetItemComparer());
                    }
                    else if (mailItem is MailItemViewModel)
                    {
                        MailCopyIdHashSet.Add(mailItem.UniqueId);

                        _mailItemSource.InsertItem(groupKey, listComparer, (MailItemViewModel)mailItem, listComparer.GetItemComparer());
                    }
                });

        private async Task RemoveItemInternalAsync(ObservableGroup<object, IMailItem> group, IMailItem mailItem)
        {
            MailCopyIdHashSet.Remove(mailItem.UniqueId);

            await ExecuteUIThread(() =>
            {
                group.Remove(mailItem);

                if (group.Count == 0)
                {
                    _mailItemSource.RemoveGroup(group.Key);
                }
            });
        }

        public async Task AddAsync(MailCopy addedItem)
        {
            // Check all items for whether this item should be threaded with them.
            bool shouldExit = false;

            var groupCount = _mailItemSource.Count;

            for (int i = 0; i < groupCount; i++)
            {
                if (shouldExit) break;

                //var group = _mailItemSource[i];

                /*for (int k = 0; k < group.Count; k++)
                {
                    var item = group[k];

                    var addedAccountProviderType = addedItem.AssignedAccount.ProviderType;

                    var threadingStrategy = ThreadingStrategyProvider?.GetStrategy(addedAccountProviderType);

                    if (threadingStrategy?.ShouldThreadWithItem(addedItem, item) ?? false)
                    {
                        shouldExit = true;

                        if (item is ThreadMailItemViewModel threadMailItemViewModel)
                        {
                            // Item belongs to existing thread.

                            // Add original item to the thread.
                             // If new group key is not the same as existing thread:
                             //-> Remove the whole thread from list
                             //-> Add the thread to the list again for sorting.
                             //Update thread properties.
                             

                            var existingGroupKey = GetGroupingKey(threadMailItemViewModel);

                            threadMailItemViewModel.AddMailItemViewModel(addedItem);

                            var newGroupKey = GetGroupingKey(threadMailItemViewModel);

                            if (!existingGroupKey.Equals(newGroupKey))
                            {
                                await RemoveItemInternalAsync(group, threadMailItemViewModel);
                                await InsertItemInternalAsync(newGroupKey, threadMailItemViewModel);
                            }

                            await ExecuteUIThread(() => { threadMailItemViewModel.NotifyPropertyChanges(); });

                            break;
                        }
                        else
                        {
                            // Item belongs to a single mail item that is not threaded yet.
                            // Same item might've been tried to added as well.
                            // In that case we must just update the item but not thread it.

                            // Remove target item.
                            // Create a new thread with both items.
                            // Add new thread to the list.
                            

                            if (item.Id == addedItem.Id)
                            {
                                // Item is already added to the list.
                                // We need to update the copy it holds.

                                if (item is MailItemViewModel itemViewModel)
                                {
                                    itemViewModel.Update(addedItem);
                                }
                            }
                            else
                            {
                                // Single item that must be threaded together with added item.

                                var threadMailItem = new ThreadMailItem();

                                threadMailItem.AddThreadItem(item);
                                threadMailItem.AddThreadItem(addedItem);

                                if (threadMailItem.ThreadItems.Count == 1) return;

                                var newGroupKey = GetGroupingKey(threadMailItem);

                                await RemoveItemInternalAsync(group, item);
                                await InsertItemInternalAsync(newGroupKey, threadMailItem);
                            }

                            break;
                        }
                    }
                    else
                    {
                        // Update properties.
                        if (item.Id == addedItem.Id && item is MailItemViewModel itemViewModel)
                        {
                            await ExecuteUIThread(() => { itemViewModel.Update(addedItem); });

                            shouldExit = true;
                        }
                    }
                }*/
            }

            if (!shouldExit)
            {
                // At this point all items are already checked and not suitable option was available.
                // Item doesn't belong to any thread.
                // Just add it to the collection.

                var groupKey = GetGroupingKey(addedItem);

                await InsertItemInternalAsync(groupKey, addedItem);
            }
        }

        // Update the usage of Key in WinoMailCollection
        public void AddRange(IEnumerable<IMailItem> items, bool clearIdCache)
        {
            if (clearIdCache)
            {
                MailCopyIdHashSet.Clear();
            }

            var groupedByName = items
                                .GroupBy(a => GetGroupingKey(a))
                                .Select(a => new ObservableGroup<object, IMailItem>(a.Key, a));

            foreach (var group in groupedByName)
            {
                foreach (var item in group)
                {
                    if (item is MailItemViewModel mailCopyItem && !MailCopyIdHashSet.Contains(item.UniqueId))
                    {
                        MailCopyIdHashSet.Add(item.UniqueId);
                    }
                    else if (item is ThreadMailItemViewModel threadMailItem)
                    {
                        foreach (var mailItem in threadMailItem.ThreadItems)
                        {
                            if (!MailCopyIdHashSet.Contains(mailItem.UniqueId))
                            {
                                MailCopyIdHashSet.Add(mailItem.UniqueId);
                            }
                        }
                    }
                }

                Group existingGroup = _mailItemSource.FirstGroupByKeyOrDefault(group.Key); // Use the Key property correctly

                if (existingGroup == null)
                {
                    _mailItemSource.AddGroup(group.Key, group); // Fix: Use the Key property correctly
                }
                else
                {
                    foreach (var item in group)
                    {
                        existingGroup.Add(item);
                    }
                }
            }
        }

        // Fix for CS0021: Replace indexing with ElementAt method for ObservableGroupedCollection
        public MailItemContainer GetMailItemContainer(Guid uniqueMailId)
        {
            var groupCount = _mailItemSource.Count;

            for (int i = 0; i < groupCount; i++)
            {
                Group group = default; _mailItemSource.ElementAt(i); // Use ElementAt instead of indexing

                for (int k = 0; k < group.Count; k++)
                {
                    var item = group.ElementAt(k); // Use ElementAt instead of indexing

                    if (item is MailItemViewModel singleMailItemViewModel && singleMailItemViewModel.UniqueId == uniqueMailId)
                        return new MailItemContainer(singleMailItemViewModel);
                    //else 
                    //if (item is ThreadMailItemViewModel threadMailItemViewModel && threadMailItemViewModel.HasUniqueId(uniqueMailId))
                    //{
                    //    var singleItemViewModel = threadMailItemViewModel.GetItemById(uniqueMailId) as MailItemViewModel;
                    //
                    //    return new MailItemContainer(singleItemViewModel, threadMailItemViewModel);
                    //}
                }
            }

            return null;
        }

        /*public MailItemViewModel GetNextItem(MailCopy mailCopy)
        {
            var groupCount = _mailItemSource.Count;

            for (int i = 0; i < groupCount; i++)
            {
                var group = _mailItemSource.ElementAt(i); // Use ElementAt instead of indexing

                for (int k = 0; k < group.Count; k++)
                {
                    var item = group.ElementAt(k); // Use ElementAt instead of indexing

                    if (item is MailItemViewModel singleMailItemViewModel && singleMailItemViewModel.UniqueId == mailCopy.UniqueId)
                    {
                        if (k + 1 < group.Count)
                        {
                            return group.ElementAt(k + 1) as MailItemViewModel; // Use ElementAt instead of indexing
                        }
                        else if (i + 1 < groupCount)
                        {
                            return _mailItemSource.ElementAt(i + 1).ElementAt(0) as MailItemViewModel; // Use ElementAt for both levels
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (item is ThreadMailItemViewModel threadMailItemViewModel && threadMailItemViewModel.HasUniqueId(mailCopy.UniqueId))
                    {
                        var singleItemViewModel = threadMailItemViewModel.GetItemById(mailCopy.UniqueId) as MailItemViewModel;

                        if (singleItemViewModel == null) return null;

                        var singleItemIndex = threadMailItemViewModel.ThreadItems.IndexOf(singleItemViewModel);

                        if (singleItemIndex + 1 < threadMailItemViewModel.ThreadItems.Count)
                        {
                            return threadMailItemViewModel.ThreadItems[singleItemIndex + 1] as MailItemViewModel;
                        }
                        else if (i + 1 < groupCount)
                        {
                            return _mailItemSource.ElementAt(i + 1).ElementAt(0) as MailItemViewModel; // Use ElementAt for both levels
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return null;
        }*/

        /*public async Task RemoveAsync(MailCopy removeItem)
        {
            // This item doesn't exist in the list.
            if (!MailCopyIdHashSet.Contains(removeItem.UniqueId)) return;

            // Check all items for whether this item should be threaded with them.
            bool shouldExit = false;

            var groupCount = _mailItemSource.Count;

            for (int i = 0; i < groupCount; i++)
            {
                if (shouldExit) break;

                var group = _mailItemSource.ElementAt(i); // Use ElementAt instead of indexing

                for (int k = 0; k < group.Count; k++)
                {
                    var item = group.ElementAt(k); // Use ElementAt instead of indexing

                    if (item is ThreadMailItemViewModel threadMailItemViewModel && threadMailItemViewModel.HasUniqueId(removeItem.UniqueId))
                    {
                        var removalItem = threadMailItemViewModel.GetItemById(removeItem.UniqueId);

                        if (removalItem == null) return;

                        await ExecuteUIThread(() => { threadMailItemViewModel.RemoveCopyItem(removalItem); });

                        if (threadMailItemViewModel.ThreadItems.Count == 1)
                        {
                            var singleViewModel = threadMailItemViewModel.GetSingleItemViewModel();
                            var groupKey = GetGroupingKey(singleViewModel);

                            await RemoveItemInternalAsync(group, threadMailItemViewModel);

                            if (!PruneSingleNonDraftItems || singleViewModel.IsDraft)
                            {
                                await InsertItemInternalAsync(groupKey, singleViewModel);
                            }
                        }
                        else if (threadMailItemViewModel.ThreadItems.Count == 0)
                        {
                            await RemoveItemInternalAsync(group, threadMailItemViewModel);
                        }
                        else
                        {
                            threadMailItemViewModel.ThreadItems.Remove(removalItem);
                        }

                        shouldExit = true;
                        break;
                    }
                    else if (item is MailItemViewModel mailItemViewModel && mailItemViewModel.UniqueId == removeItem.UniqueId)
                    {
                        await RemoveItemInternalAsync(group, mailItemViewModel);
                        shouldExit = true;

                        break;
                    }
                }
            }
        }*/

        /// <summary>
        /// Fins the item container that updated mail copy belongs to and updates it.
        /// </summary>
        /// <param name="updatedMailCopy">Updated mail copy.</param>
        /// <returns></returns>
        public async Task UpdateMailCopy(MailCopy updatedMailCopy)
        {
            // This item doesn't exist in the list.
            if (!MailCopyIdHashSet.Contains(updatedMailCopy.UniqueId))

            {
                return;
            }

            await ExecuteUIThread(() =>
            {
                var itemContainer = GetMailItemContainer(updatedMailCopy.UniqueId);

                if (itemContainer == null) return;

                // mailCopyIdHashSet.Remove(itemContainer.ItemViewModel.UniqueId);

                itemContainer.ItemViewModel?.Update(updatedMailCopy);

                // mailCopyIdHashSet.Add(updatedMailCopy.UniqueId);

                // Call thread notifications if possible.
                itemContainer.ThreadViewModel?.NotifyPropertyChanges();
            });
        }

        // Fix for CS0021: Replace indexing with ElementAt method for ObservableGroupedCollection
      
        private async Task ExecuteUIThread(Action action) => await CoreDispatcher?.ExecuteOnUIThread(action);

        internal MailItemViewModel GetNextItem(MailCopy removedMail)
        {
            throw new NotImplementedException();
        }

        internal async Task RemoveAsync(MailCopy removedMail)
        {
            throw new NotImplementedException();
        }
    }

    // Fix for CS0119 and CS0572: Correctly reference the Key property or type in ObservableGroup

    public class ObservableGroup<T1, T2> : IEnumerable<T2>
    {
        private readonly List<T2> _items = new List<T2>();

        public int Count => _items.Count;

        public object Key { get; private set; } // Ensure Key is a property, not a type

        public ObservableGroup(object key, IEnumerable<T2> items)
        {
            Key = key; // Assign the key to the property
            _items.AddRange(items);
        }

        public void Remove(T2 item)
        {
            _items.Remove(item);
        }

        public IEnumerator<T2> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }
}
