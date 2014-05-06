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
            _repository = new MemSermonRepository();
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
            Assert.AreEqual(3, _repository.GetSermons().Count(),"wrong number of sermons found");
            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "The Test Sermon" 
                                                                    && s.SermonPreacher.FirstName == "John" 
                                                                    && s.SermonPreacher.LastName == "Smith"
                                                                    && s.SermonLocation.City == "Albuquerque"
                                                                    && s.SermonLocation.State == "NM"
                                                                    && s.SermonLocation.Venue == "Church of Christ"
                                                                    && string.IsNullOrEmpty(s.Comment)
                                                                    && string.IsNullOrEmpty(s.Passages)),"The Test Sermon not found");

            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "Luke 12a"
                                                                    && s.SermonPreacher.FirstName == "Bob"), "Luke 12a not found");

            Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is A Test Sermon"
                                                                    && s.SermonPreacher.FirstName == "Bill"),"This is a Test Sermon not found");
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
    }
}
