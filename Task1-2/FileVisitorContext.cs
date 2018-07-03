using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    public class FileVisitorContext
    {
        public FileVisitorContext(FileSystemVisitorBase fileVisitorStrategy)
        {
            this.FileVisitorStrategy = fileVisitorStrategy;
        }
        
        public IEnumerable<string> GetAllFilesAndDirectories()
        {
            return FileVisitorStrategy.GetAllItems();
        }
        public FileSystemVisitorBase FileVisitorStrategy { get; private set; }
    }
}
