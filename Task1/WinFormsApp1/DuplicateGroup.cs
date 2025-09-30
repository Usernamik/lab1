
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
    public class DuplicateGroup
    {
        public string Hash { get; set; }
        public long Size { get; set; }
        public List<FileMetaData> Files { get; set; } = new List<FileMetaData>();

        public void MarkOriginal()
        {
            // Позначу найстаріший файл як оригінал
            if (Files.Count > 0)
            {
                var original = Files.OrderBy(f => f.CreationTime).First();
                original.IsOriginal = true;

                foreach (var file in Files.Where(f => f != original))
                {
                    file.IsOriginal = false;
                }
            }
        }
    }
}
        
