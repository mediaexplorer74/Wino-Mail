using Wino.Core.Domain.Models.MailItem;

namespace Wino.Mail.ViewModels.Collections
{
    public class ReadOnlyObservableGroupedCollection<T1, T2>
    {
        private ObservableGroupedCollection<object, IMailItem> mailItemSource;

        public ReadOnlyObservableGroupedCollection(ObservableGroupedCollection<object, IMailItem> mailItemSource)
        {
            this.mailItemSource = mailItemSource;
        }
    }
}