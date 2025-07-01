using System;
using Wino.Core.Domain.Entities;

namespace Wino.Mail.ViewModels
{
    public class Account
    {
        public Preferences Preferences;
        public Guid Id;
        public string Name;
        public MailAccount account;
    }
}