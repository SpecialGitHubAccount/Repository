using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    public class FileSystemVisitor : FileSystemVisitorBase
    {
        public FileSystemVisitor(string path) : this(path, null)
        {

        }

        public FileSystemVisitor(string path, Func<string, bool> filter) : base(path, filter)
        {

        }

        public override IEnumerable<string> GetAllItems()
        {
            RaiseStart();
            if (filter == null)
            {
                return GetFilesAndDirectories();
            }
            else
            {
                return GetFilteredFilesAndDirectories();
            }
        }
        
        private IEnumerable<string> GetFilesAndDirectories()
        {
            // После каждого найденной папки/файла будет происходить событие.
            foreach (string directory in Directory.GetDirectories(path))
            {
                var eventArgs = new FileOrDirectoryFindedEventArgs
                {
                    isBreak = false,
                    isExclude = false,
                    itemName = directory
                };

                RaiseDirectoryFinded(eventArgs);
                // Если пользовтель захочет исключить найденный файл - он не будет включен в итоговую коллекцию.
                if (!eventArgs.isExclude)
                {
                    yield return directory;
                }
                // Если пользователь решил прервать дальнейший поиск.
                if (eventArgs.isBreak)
                {
                    yield break;
                }
            }

            foreach (var file in Directory.GetFiles(path))
            {
                var eventArgs = new FileOrDirectoryFindedEventArgs
                {
                    isBreak = false,
                    isExclude = false,
                    itemName = file
                };

                RaiseDirectoryFinded(eventArgs);

                if (eventArgs.isBreak)
                {
                    yield break;
                }
                if (!eventArgs.isExclude)
                {
                    yield return file;
                }
            }

            RaiseEnd();
        }

        private IEnumerable<string> GetFilteredFilesAndDirectories()
        {
            foreach (var directory in Directory.GetDirectories(path).Where(filter))
            {
                var eventArgs = new FileOrDirectoryFindedEventArgs
                {
                    isBreak = false,
                    isExclude = false,
                    itemName = directory
                };

                RaiseFilteredDirectoryFinded(eventArgs);

                if (eventArgs.isBreak)
                {
                    yield break;
                }
                if (!eventArgs.isExclude)
                {
                    yield return directory;
                }
            }

            foreach (var file in Directory.GetFiles(path).Where(filter))
            {
                var eventArgs = new FileOrDirectoryFindedEventArgs
                {
                    isBreak = false,
                    isExclude = false,
                    itemName = file
                };

                RaiseFilteredFileFinded(eventArgs);

                if (!eventArgs.isExclude)
                {
                    yield return file;
                }

                if (eventArgs.isBreak)
                {
                    yield break;
                }
            }

            RaiseEnd();
        }
        
        
    }
}
