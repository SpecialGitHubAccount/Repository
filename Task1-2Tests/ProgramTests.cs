using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Task1_2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        /// <summary>
        /// Ожидаем, что без фильтра fake-object вернет все файлы
        /// </summary>
        [TestMethod()]
        public void NonFilteredFilesAndDirectories()
        {
            FileVisitorContext visitorContext = new FileVisitorContext(new FakeFileVisitor());

            string[] exptected =
            {
                "folder1",
                "folder2",
                "app1.exe",
                "app2.exe",
                "app3.exe",
                "image1.jpg",
                "image2.png",
                "sound1.mp3",
                "movie1.mp4",
                "app4.exe"
            };

            IEnumerable<string> actual = visitorContext.GetAllFilesAndDirectories();
            
            CollectionAssert.AreEqual(exptected, actual.ToArray());
        }

        /// <summary>
        /// Ожидается, что с фильтром будут возвращены только файлы с расширением .exe
        /// </summary>
        [TestMethod()]
        public void FilteredFilesAndDirectories()
        {
            FileVisitorContext visitorContext = new FileVisitorContext(new FakeFileVisitor((s) => s.EndsWith(".exe") ));
            string[] exptected =
            {
                "app1.exe",
                "app2.exe",
                "app3.exe",
                "app4.exe"
            };

            IEnumerable<string> actual = visitorContext.GetAllFilesAndDirectories();
            CollectionAssert.AreEqual(exptected, actual.ToArray());
        }
    }
}