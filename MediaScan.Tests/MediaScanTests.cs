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
            var johnMp3 = File.Create(@"Sermons\JohnSmith_TestSermon.mp3");
            johnMp3.Close();
            var bobMp3 = File.Create(@"Sermons\Bob_ExampleSermon.mp3");
            bobMp3.Close();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete("Sermons", true);
        }


        [TestMethod]
        public void ItCanCreateASermonFromAFilename()
        {
            MediaScan mediaScan = new MediaScan("Sermons", _repository);
            Assert.AreEqual(2, _repository.GetSermons().ToList().Count);
            Assert.IsTrue(_repository.GetSermons().ToList().Exists(s => s.Title == "Test Sermon" 
                                                                    && s.SermonPreacher.FirstName == "John" 
                                                                    && s.SermonPreacher.LastName == "Smith"
                                                                    && s.SermonLocation.City == "Albuquerque"
                                                                    && s.SermonLocation.State == "NM"
                                                                    && s.SermonLocation.Venue == "Church of Christ"
                                                                    && string.IsNullOrEmpty(s.Comment)
                                                                    && string.IsNullOrEmpty(s.Passages)));
        }
    }
}
