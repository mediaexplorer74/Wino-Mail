using System;
using Wino.Core.Domain.Enums;
using Wino.Core.Domain.Models.Folders;

namespace Wino.Core.MenuItems
{
    internal class Parameter
    {
        internal static Guid Id;
        internal static string Name;
        internal static bool ShowUnreadCount;
        internal static bool IsSynchronizationEnabled;
        internal static string FolderName;
        internal static SpecialFolderType SpecialFolderType;
        internal static bool IsSticky;
        internal static bool IsSystemFolder;
        internal static string TextColorHex;

        internal static void Add(IMailItemFolder folder)
        {
            throw new NotImplementedException();
        }

        internal static object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        internal static void Remove(IMailItemFolder existingFolder)
        {
            throw new NotImplementedException();
        }
    }
}