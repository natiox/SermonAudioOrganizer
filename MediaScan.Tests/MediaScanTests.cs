using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SermonAudioOrganizer.Domain;

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
        }


        [TestMethod]
        public void ItCanCreateASermonFromAFilename()
        {
            MediaScan mediaScan = new MediaScan(@"E:\temp\", _repository);
            Assert.AreEqual(1, mediaScan.Sermons.Count);
            Assert.IsTrue(mediaScan.Sermons.Exists(s => s.Title == "Test Sermon" && s.SermonPreacher.FirstName == "John" && s.SermonPreacher.LastName == "Smith"));
        }
    }
}
