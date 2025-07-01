using Microsoft.Graph.Models;
using System.Collections.Generic;
using Wino.Core.Domain.Entities;

namespace Wino.Mail.ViewModels
{
    public class MergedAccount
    {
        public MergedInbox MergedInbox;
        public List<Account> HoldingAccounts;
    }
}