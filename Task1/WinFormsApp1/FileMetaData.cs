using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace WinFormsApp1
{
    public class FileMetaData
    {
        public string FullPath { get; set; }
        public long Size { get; set; }
        public string Hash { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsOriginal { get; set; } = false;

        public FileMetaData(string Path)
        {
            FullPath = Path;
            var info = new FileInfo(Path);
            Size = info.Length;
            CreationTime = info.CreationTime;
        }

        public void ComputeHash()
        {
            try
            {
                using (var stream = File.OpenRead(FullPath))
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
            catch (Exception)
            {
                Hash = string.Empty;
            }
        }
    }
}
