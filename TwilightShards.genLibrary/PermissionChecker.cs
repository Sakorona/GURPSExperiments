using System;
using System.IO;
using System.Security.AccessControl;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This is a helper object for checking for permissions to the file
    /// </summary>
    public class PermissionChecker
    {
        /// <summary>
        /// Finds if the program has write permissions to this folder
        /// </summary>
        /// <param name="path">The folder to be tested</param>
        /// <returns>True for write permissions; False for none</returns>
        public static bool HasWritePermissionOnDir(string path)
        {
            var writeAllow = false;
            var writeDeny = false;
            try
            {
                var accessControlList = Directory.GetAccessControl(path);
                if (accessControlList == null)
                    return false;
                var accessRules = accessControlList.GetAccessRules(true, true,
                                            typeof(System.Security.Principal.SecurityIdentifier));
                if (accessRules == null)
                    return false;

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                        continue;

                    if (rule.AccessControlType == AccessControlType.Allow)
                        writeAllow = true;
                    else if (rule.AccessControlType == AccessControlType.Deny)
                        writeDeny = true;
                }

                return writeAllow && !writeDeny;
            }
            catch (UnauthorizedAccessException)
            {
                //the user cannot access the rights.
                return false;
            }
        }

        /// <summary>
        /// Finds if this program has read permissions in this folder
        /// </summary>
        /// <param name="path">The folder to be tested</param>
        /// <returns>True for read permissions; False for none</returns>
        public static bool HasReadPermissionOnDir(string path)
        {
            var readAllow = false;
            var readDeny = false;
            try
            {
                var accessControlList = Directory.GetAccessControl(path);
                if (accessControlList == null)
                    return false;
                var accessRules = accessControlList.GetAccessRules(true, true,
                                            typeof(System.Security.Principal.SecurityIdentifier));
                if (accessRules == null)
                    return false;

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read)
                        continue;

                    if (rule.AccessControlType == AccessControlType.Allow)
                        readAllow = true;
                    else if (rule.AccessControlType == AccessControlType.Deny)
                        readDeny = true;
                }

                return readAllow && !readDeny;
            }
            catch (UnauthorizedAccessException)
            {
                //the user cannot access the rights.
                return false;
            }
        }
    }
}
