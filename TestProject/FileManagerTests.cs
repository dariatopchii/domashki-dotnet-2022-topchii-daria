using System;
using System.IO;
using dotnet_2022_study;
using NUnit.Framework;

namespace TestProject
{
    public class FileManagerTests
    {
        private StringWriter _output;
        private const string TestFolder = "../../../testing_folder";
        private FileManager _fileManager;
        

        [SetUp]
        public void Setup()
        {
            _fileManager = new FileManager
            {
                Path = TestFolder
            };
            _output = new StringWriter();
        }

        [Test]
        public void CreateFile_FileNotExists_Creates()
        {
            // setup
            
            var filename = "test" ;
            Console.SetOut(_output);

            // act
            _fileManager.CreateFile(filename);

            // assert
            Assert.True(File.Exists($"{TestFolder}/{filename}"));
            Assert.AreEqual($"{filename} created in {TestFolder}{Environment.NewLine}", _output.ToString());
            
            
            // cleanup
            _fileManager.DeleteFile(filename);
        }
        
        [Test]
        public void CreateFile_FileExists_CreatesDouble()
        {
            // setup
            
            var filename = "test1" ;
            _fileManager.CreateFile(filename);
            Console.SetOut(_output);

            // act
            _fileManager.CreateFile(filename);
            // assert
            Assert.True(File.Exists($"{TestFolder}/{filename}"));
            Assert.AreEqual($"{filename} already exists in {TestFolder}{Environment.NewLine}", _output.ToString());
            
            
            // cleanup
            _fileManager.DeleteFile(filename);
        }

        [Test]
        public void DeleteFIle_FileExists_Deletes()
        {
            // setup
            
            var filename = "test2";
            _fileManager.CreateFile(filename);
            Console.SetOut(_output);

            
            // act
            _fileManager.DeleteFile(filename);
            
            // assert
            Assert.False(File.Exists($"{TestFolder}/{filename}"));
            Assert.AreEqual($"{filename} deleted in {TestFolder}{Environment.NewLine}", _output.ToString());
        }
        
        [Test]
        public void DeleteFIle_FileNotExists_Deletes()
        {
            // setup
            var filename = "test3";
            Console.SetOut(_output);
            

            
            // act
            _fileManager.DeleteFile(filename);
            
            // assert
            Assert.False(File.Exists($"{TestFolder}/{filename}"));
            Assert.AreEqual($"{filename} doesn`t exist in {TestFolder}{Environment.NewLine}", _output.ToString());
        }

        [Test]
        public void RenameFile_FileName_Changes()
        {
            //setup
            
            var oldname = "test4";
            var newname = "test5";
            _fileManager.CreateFile(oldname);
            
            //act
            _fileManager.RenameFile(oldname, newname);
            
            //assert
            Assert.True(File.Exists($"{TestFolder}/{newname}"));
            
            //cleanup
            _fileManager.DeleteFile(newname);
        }
        
        [Test]
        public void RenameFile_FileNotExists_Changes()
        {
            //setup
            
            var oldname = "test6";
            var newname = "test7";
            Console.SetOut(_output);
            //act
            _fileManager.RenameFile(oldname, newname);
            
            //assert
            Assert.AreEqual($"{oldname} file doesn`t exist in {TestFolder}{Environment.NewLine}", _output.ToString());
            
            //cleanup
            _fileManager.DeleteFile(newname);
        }

        [Test]
        public void CreateDirectory_DirectoryNotExists_Creates()
        {
            //setup
            
            var dirname = "test8" ;
            
            // act
            _fileManager.CreateDirectory(dirname);
            
            // assert
            Assert.True(Directory.Exists($"{TestFolder}/{dirname}"));
            
            // cleanup
            _fileManager.DeleteDirectory(dirname);
        }


        [Test]
        public void DeleteDirectory_DirectoryExists_Deletes()
        {
            // setup
            
            var dirname = "test9";
            _fileManager.CreateDirectory(dirname);
            
            // act
            _fileManager.DeleteDirectory(dirname);
            
            // assert
            Assert.False(Directory.Exists($"{TestFolder}/{dirname}"));
        }
        
        [Test]
        public void RenameDirectory_DirectoryName_Changes()
        {
            //setup
            
            var oldname = "test10";
            var newname = "test11";
            _fileManager.CreateDirectory(oldname);
            
            //act
            _fileManager.RenameDirectory(oldname, newname);
            
            //assert
            Assert.True(Directory.Exists($"{TestFolder}/{newname}"));
            
            //cleanup
            _fileManager.DeleteDirectory(oldname);
            _fileManager.DeleteDirectory(newname);
        }

       
        [Test]
        public void DirectoryInfo_Directory_ShowsInfo()
        {
            //setup
            
            Console.SetOut(_output);
            string[] testinput = {"ls"};
            //act
            _fileManager.DirectoryInfo(testinput);
            //assert
            Assert.True(_output.ToString().Contains("\\test_file"));
            
        }


        [Test]
        public void FindSubstring_SearchesForString()
        {
            //setup
            
            Console.SetOut(_output);
            string test_file = "test_file";
            
            //act
            _fileManager.FindSubstring(test_file, "dml");
           
            
            //assert
            Assert.AreEqual($"there is a match{Environment.NewLine}", _output.ToString());
        }
    }
}