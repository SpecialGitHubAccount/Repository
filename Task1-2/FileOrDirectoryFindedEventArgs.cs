using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    public class FileOrDirectoryFindedEventArgs : EventArgs
    {
        public bool isBreak { get; set; }
        public bool isExclude { get; set; }
        public string itemName { get; set; }
    }
}
