using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    /// <summary>
    /// Fake object для тестов, не зависящий от файловой системы ОС.
    /// </summary>
    public class FakeFileVisitor : FileSystemVisitorBase
    {
        static FakeFileVisitor()
        {
            fakeFilesAndDirectories = new List<string>();
            fakeFilesAndDirectories.Add("folder1");
            fakeFilesAndDirectories.Add("folder2");
            fakeFilesAndDirectories.Add("app1.exe");
            fakeFilesAndDirectories.Add("app2.exe");
            fakeFilesAndDirectories.Add("app3.exe");
            fakeFilesAndDirectories.Add("image1.jpg");
            fakeFilesAndDirectories.Add("image2.png");
            fakeFilesAndDirectories.Add("sound1.mp3");
            fakeFilesAndDirectories.Add("movie1.mp4");
            fakeFilesAndDirectories.Add("app4.exe");
        }

        public FakeFileVisitor() : base(null, null)
        {

        }

        public FakeFileVisitor(Func<string, bool> filter) : base(null, filter)
        {

        }

        public override IEnumerable<string> GetAllItems()
        {
            if (filter == null)
            {
                foreach (string directory in fakeFilesAndDirectories)
                {
                    yield return directory;
                }
            }
            else
            {
                foreach (string directory in fakeFilesAndDirectories.Where(filter))
                {
                    yield return directory;
                }
            }
        }        

        /// <summary>
        /// Список имен файлов и папок.
        /// </summary>
        private static List<string> fakeFilesAndDirectories;
    }
}
