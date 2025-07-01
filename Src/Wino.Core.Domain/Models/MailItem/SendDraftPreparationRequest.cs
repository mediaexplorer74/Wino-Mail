using MimeKit;
using Wino.Core.Domain.Entities;

namespace Wino.Core.Domain.Models.MailItem
{
    public record SendDraftPreparationRequest(MailCopy MailItem, MimeMessage Mime, MailItemFolder DraftFolder, MailItemFolder SentFolder, MailAccountPreferences AccountPreferences)
    {
        private MailCopy mailCopy;
        private MimeMessage currentMimeMessage;
        private MailItemFolder assignedFolder;
        private MailItemFolder sentFolder;
        private Preferences preferences;

        // Constructor with 'this' initializer to fix CS8862
        public SendDraftPreparationRequest(MailCopy mailCopy, MimeMessage currentMimeMessage, MailItemFolder assignedFolder, MailItemFolder sentFolder, Preferences preferences)
            : this(mailCopy, currentMimeMessage, assignedFolder, sentFolder, null as MailAccountPreferences) // Explicitly cast null to resolve ambiguity
        {
            this.mailCopy = mailCopy;
            this.currentMimeMessage = currentMimeMessage;
            this.assignedFolder = assignedFolder;
            this.sentFolder = sentFolder;
            this.preferences = preferences;
        }
    }
}
