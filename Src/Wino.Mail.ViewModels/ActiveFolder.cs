using System;
using System.Collections.Generic;
using Wino.Core.Domain.Entities;
using Wino.Core.Domain.Enums;

namespace Wino.Mail.ViewModels
{
    internal class ActiveFolder
    {
        internal SpecialFolderType SpecialFolderType;
        internal IEnumerable<Folder> HandlingFolders;
        internal bool IsSynchronizationEnabled;
        internal string FolderName;

        internal void UpdateFolder(MailItemFolder updatedFolder)
        {
            throw new NotImplementedException();
        }
    }
}