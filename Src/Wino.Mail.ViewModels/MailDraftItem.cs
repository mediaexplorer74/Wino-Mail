using System;
using Wino.Core.Domain.Entities;

namespace Wino.Mail.ViewModels
{
    internal class MailDraftItem
    {
        public string DraftId;
        public Account AssignedAccount;
        internal MailCopy MailCopy;
        internal MailItemFolder AssignedFolder;
        internal string Subject;
        internal string PreviewText;
        internal bool IsLocalDraft;
        internal string Id;
        internal Guid UniqueId;

        internal void Update(MailCopy updatedMail)
        {
            throw new NotImplementedException();
        }
    }
}