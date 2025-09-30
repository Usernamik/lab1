using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class FileHelper
    {
        public static string GetFileNameWithoutExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            int lastSlashIndex = filePath.LastIndexOfAny(new char[] { '\\', '/' });
            int lastDotIndex = filePath.LastIndexOf('.');
            if (lastDotIndex == -1 || (lastSlashIndex != -1 && lastDotIndex < lastSlashIndex))
            {
                lastDotIndex = filePath.Length; 
            }
            if (lastSlashIndex == -1)
            {
                lastSlashIndex = -1; 
            }
            return filePath.Substring(lastSlashIndex + 1, lastDotIndex - lastSlashIndex - 1);
        }
    }
}