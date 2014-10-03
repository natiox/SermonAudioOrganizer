using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SermonAudioOrganizer.Domain;

//using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MediaScan.Tests
{
    [TestClass]
    public class MediaScanUnitTest
    {
        ISermonRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            //_repository = new MemSermonRepository();
            //This ain't working.  
            _repository = new EFSermonRepository(new SermonContext());
            Directory.CreateDirectory("Sermons");
            var johnMp3 = File.Create(@"Sermons\JohnSmith_TheTestSermon.mp3");
            johnMp3.Close();
            var bobMp3 = File.Create(@"Sermons\Bob_Luke_12a.mp3");
            bobMp3.Close();
            var billMp3 = File.Create(@"Sermons\Bill_This_Is_A_Test_Sermon.mp3");
            billMp3.Close();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete("Sermons", true);
        }


        [TestMethod]
        public void ItCanCreateSermonsFromFilenames()
        {
            MediaScan mediaScan = new MediaScan("Sermons", _repository);
            Assert.AreEqual(3, _repository.GetSermons().Count(), "wrong number of sermons found");
            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "The Test Sermon"
                                                                    && s.SermonPreacher.FirstName == "John"
                                                                    && s.SermonPreacher.LastName == "Smith"
                                                                    && s.SermonLocation.City == "Albuquerque"
                                                                    && s.SermonLocation.State == "NM"
                                                                    && s.SermonLocation.Venue == "Church of Christ"
                                                                    && string.IsNullOrEmpty(s.Comment)
                                                                    && string.IsNullOrEmpty(s.Passages)), "The Test Sermon not found");

            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "Luke 12a"
                                                                    && s.SermonPreacher.FirstName == "Bob"), "Luke 12a not found");

            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is A Test Sermon"
                                                                    && s.SermonPreacher.FirstName == "Bill"), "This is a Test Sermon not found");
        }

        [TestMethod]
        public void ItCanLookUpPreachersByFirstName()
        {
            //Arrange
            _repository.InsertPreacher(new Preacher() { FirstName = "Bill", LastName = "Wyatt" });
            _repository.Save();

            //Act
            MediaScan mediaScan = new MediaScan("Sermons", _repository);

            //Assert
            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is A Test Sermon"
                                                                    && s.SermonPreacher.FirstName == "Bill"
                                                                    && s.SermonPreacher.LastName == "Wyatt"), "Preacher Bill Wyatt not found");
        }

        [TestMethod]
        public void ItAvoidsDuplicateSermons()
        {
            //Arrange
            MediaScan mediaScan = new MediaScan("Sermons", _repository);

            //Act
            mediaScan = new MediaScan("Sermons", _repository);

            //Assert
            Assert.AreEqual(_repository.GetSermons().Count(s => s.Title == "This Is A Test Sermon"), 1, "Duplicate sermons found");
        }

        [TestMethod]
        public void ItAvoidsDuplicatePreachers()
        {
            //Arrange
            if (!_repository.GetPreachers().Any(p => p.FirstName == "Unit" && p.LastName == "Test"))
            {
                _repository.InsertPreacher(new Preacher() { FirstName = "Unit", LastName = "Test" });
                _repository.Save();
            }
            MediaScan mediaScan = new MediaScan("Sermons", _repository);

            var bill2Mp3 = File.Create(@"Sermons\Unit_This_Is_Another_Test_Sermon.mp3");
            bill2Mp3.Close();

            File.Delete(bill2Mp3.ToString());

            //Act
            mediaScan = new MediaScan("Sermons", _repository);

            //Assert
            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is Another Test Sermon"
                                                                    && s.SermonPreacher.FirstName == "Unit"
                                                                    && s.SermonPreacher.LastName == "Test"), "Preacher Unit Test not found");

            //TODO: WHEREYOUWERE MediaScan is creating duplicate preachers, regardless of what this says.
            Assert.AreEqual(_repository.GetPreachers().Count(p => p.FirstName == "Unit"), 1, "Duplicate preachers found");
        }

        [TestMethod]
        public void ASermonCanBeDeleted()
        {
            //Arrange
            Sermon sermon = new Sermon() { Title = "Sermon to Delete" };
            _repository.InsertSermon(sermon);
            //_repository.InsertPreacher(new Preacher() { FirstName = "John", LastName = "Smith" });
            _repository.Save();

            //Act
            _repository.DeleteSermon(sermon.Id);

            //Assert
            Assert.IsFalse(_repository.GetSermons().Any(s => s.Title == "Sermon to Delete"), "Sermon to Delete not found");
        }
    }
}
