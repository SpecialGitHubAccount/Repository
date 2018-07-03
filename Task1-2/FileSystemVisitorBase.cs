using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    /// <summary>
    /// Класс, содержащий общие члены для FileSystemVistor.
    /// </summary>
    public abstract class FileSystemVisitorBase
    {
        public FileSystemVisitorBase(string path, Func<string, bool> filter)
        {
            this.path = path;
            this.filter = filter;
        }
        /// <summary>
        /// Метод, возвращающий все найденные файлы и папки в директории указанной path, с подержкой фильтрации.
        /// </summary>
        /// <returns>Type: string. Объект-перечислитель элементы которого - имена файлов/папок</returns>
        public abstract IEnumerable<string> GetAllItems();

        public event EventHandler Start;
        public event EventHandler End;
        public event FileOrDirectoryFindedHandler FileFinded;
        public event FileOrDirectoryFindedHandler DirectoryFinded;
        public event FileOrDirectoryFindedHandler FilteredFileFinded;
        public event FileOrDirectoryFindedHandler FilteredDirectoryFinded;

        protected virtual void RaiseStart()
        {
            if (Start != null)
            {
                Start(this, new EventArgs());
            }
        }
        protected virtual void RaiseEnd()
        {
            if (End != null)
            {
                End(this, new EventArgs());
            }
        }
        protected virtual void RaiseFileFinded(FileOrDirectoryFindedEventArgs args)
        {
            if (FileFinded != null)
            {
                FileFinded(this, args);
            }
        }
        protected virtual void RaiseDirectoryFinded(FileOrDirectoryFindedEventArgs args)
        {
            if (DirectoryFinded != null)
            {
                DirectoryFinded(this, args);
            }
        }
        protected virtual void RaiseFilteredFileFinded(FileOrDirectoryFindedEventArgs args)
        {
            if (FilteredFileFinded != null)
            {
                FilteredFileFinded(this, args);
            }
        }
        protected virtual void RaiseFilteredDirectoryFinded(FileOrDirectoryFindedEventArgs args)
        {
            if (FilteredDirectoryFinded != null)
            {
                FilteredDirectoryFinded(this, args);
            }
        }
        
        /// <summary>
        /// Делегат фильтра, который принимает имя файла/папки, и возвращает результат предиката.
        /// </summary>
        protected readonly Func<string, bool> filter;
        
        protected readonly string path;
    }
}
