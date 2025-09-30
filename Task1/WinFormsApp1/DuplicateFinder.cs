using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class DuplicateFinder
    {
        public async Task<List<DuplicateGroup>> FindDuplicatesAsync(string rootPath)
        {
            return await Task.Run(() => FindDuplicates(rootPath));
        }

        private List<DuplicateGroup> FindDuplicates(string rootPath)
        {
            var allFiles = new List<FileMetaData>();

            //  всі файли з папки та підпапок (рекурсивно)
            try
            {
                var filePaths = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);

                foreach (var filePath in filePaths)
                {
                    try
                    {
                        var fileMetadata = new FileMetaData(filePath);
                        allFiles.Add(fileMetadata);
                    }
                    catch
                    {
                        // Ігноруємо файли, до яких немає доступу
                    }
                }
            }
            catch (Exception)
            {
                // Ігноруємо помилки доступу до папок
            }

            // Групуємо за розміром
            var groupsBySize = allFiles.GroupBy(f => f.Size)
                                       .Where(g => g.Count() > 1)
                                       .ToList();

            var duplicateGroups = new List<DuplicateGroup>();

            // Для кожної групи однакового розміру обчислюємо хеш
            foreach (var sizeGroup in groupsBySize)
            {
                foreach (var file in sizeGroup)
                {
                    file.ComputeHash();
                }

                // Групуємо за хешем, якщо файли мають різний розмір, вони гарантовано не є дублікатами
                var hashGroups = sizeGroup.GroupBy(f => f.Hash)
                                          .Where(g => g.Count() > 1 && !string.IsNullOrEmpty(g.Key))
                                          .ToList();

                foreach (var hashGroup in hashGroups)
                {
                    var group = new DuplicateGroup
                    {
                        Hash = hashGroup.Key,
                        Size = hashGroup.First().Size,
                        Files = hashGroup.ToList()
                    };

                    duplicateGroups.Add(group);
                }
            }

            return duplicateGroups;
        }

        public bool DeleteDuplicate(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
            }
            catch
            {
                // Не вдалося видалити файл
            }

            return false;
        }
    }
}