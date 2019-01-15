using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ErrorProneWebsite.Models;

namespace ErrorProneWebsite.Tests
{
    [TestClass]
    public class FileManagerTest
    {
        private const string TEST_FILE_PATH = @"C:\Users\Sulur\Documents\Desktop\New folder\Week17-ErrorHandlingWebsite\ErrorProneWebsite.Tests\TestContent\TestContent.txt";
        
        [TestMethod]
        public void TheFileManagerCanReadAFile()
        {
            FileManager fileManager = new FileManager(TEST_FILE_PATH);

            Assert.AreEqual("Here is some test content.", fileManager.GetContent());


        }
    }
}
