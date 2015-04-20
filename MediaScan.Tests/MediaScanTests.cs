﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SermonAudioOrganizer.Domain;

//using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data.Entity;
using Moq;
using System.Collections.Generic;

namespace MediaScan.Tests
{
    [TestClass]
    public class MediaScanUnitTest
    {
        private Mock<SermonContext> mockSermonContext;

        private Mock<DbSet<Sermon>> mockSermonSet;
        private Mock<DbSet<Preacher>> mockPreacherSet;
        private Mock<DbSet<Media>> mockMediaSet;
        private Mock<DbSet<Series>> mockSeriesSet;
        private Mock<DbSet<Section>> mockSectionSet;
        private Mock<DbSet<Location>> mockLocationSet;

        [TestInitialize]
        public void Initialize()
        {
            //Set up database file path
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures")));

            //Note to self:  to get this EF repository to work, I had to initially set connection strings in temp folder to create mdf file.
            //From there, I was able to copy it into the bin\Debug folder.  Might have to do this on a new machine too?
            //_repository = new MemSermonRepository();
            //This ain't working.  


            mockSermonContext = new Mock<SermonContext>();

            mockSermonSet = new Mock<DbSet<Sermon>>();
            var sermonList = new List<Sermon> 
            { 
                new Sermon 
                {
                    Id = 1,
                    Title = "Test",
                    Topic = "Test Topic",
                    Comment = "Test Comment",
                    RecordingDate = DateTime.Today,
                    Passages = "Test Passages",
                    SeriesIndex = "1",
                    SectionIndex = 1
                }
            }.AsQueryable(); 
            mockSermonSet.As<IQueryable<Sermon>>().Setup(m => m.Provider).Returns(sermonList.Provider);
            mockSermonSet.As<IQueryable<Sermon>>().Setup(m => m.Expression).Returns(sermonList.Expression);
            mockSermonSet.As<IQueryable<Sermon>>().Setup(m => m.ElementType).Returns(sermonList.ElementType);
            mockSermonSet.As<IQueryable<Sermon>>().Setup(m => m.GetEnumerator()).Returns(sermonList.GetEnumerator());

            mockPreacherSet = new Mock<DbSet<Preacher>>();
            var preacherList = new List<Preacher> 
            { 
                new Preacher 
                {
                    Id = 1,
                    FirstName = "Bill",
                    LastName="Wyatt"
                }
            }.AsQueryable();
            mockPreacherSet.As<IQueryable<Preacher>>().Setup(m => m.Provider).Returns(preacherList.Provider);
            mockPreacherSet.As<IQueryable<Preacher>>().Setup(m => m.Expression).Returns(preacherList.Expression);
            mockPreacherSet.As<IQueryable<Preacher>>().Setup(m => m.ElementType).Returns(preacherList.ElementType);
            mockPreacherSet.As<IQueryable<Preacher>>().Setup(m => m.GetEnumerator()).Returns(preacherList.GetEnumerator());

            mockMediaSet = new Mock<DbSet<Media>>();
            var mediaList = new List<Media> 
            { 
                new Media 
                {
                    Id = 1,
                    Name = "BillWyatt_TestLesson.mp3",
                    Type = MediaType.MP3
                }
            }.AsQueryable();
            mockMediaSet.As<IQueryable<Media>>().Setup(m => m.Provider).Returns(mediaList.Provider);
            mockMediaSet.As<IQueryable<Media>>().Setup(m => m.Expression).Returns(mediaList.Expression);
            mockMediaSet.As<IQueryable<Media>>().Setup(m => m.ElementType).Returns(mediaList.ElementType);
            mockMediaSet.As<IQueryable<Media>>().Setup(m => m.GetEnumerator()).Returns(mediaList.GetEnumerator());

            mockSeriesSet = new Mock<DbSet<Series>>();
            var seriesList = new List<Series> 
            { 
                new Series 
                {
                    Id = 1,
                    Title = "Test Series"
                }
            }.AsQueryable();
            mockSeriesSet.As<IQueryable<Series>>().Setup(m => m.Provider).Returns(seriesList.Provider);
            mockSeriesSet.As<IQueryable<Series>>().Setup(m => m.Expression).Returns(seriesList.Expression);
            mockSeriesSet.As<IQueryable<Series>>().Setup(m => m.ElementType).Returns(seriesList.ElementType);
            mockSeriesSet.As<IQueryable<Series>>().Setup(m => m.GetEnumerator()).Returns(seriesList.GetEnumerator());

            mockSectionSet = new Mock<DbSet<Section>>();
            var sectionList = new List<Section> 
            { 
                new Section 
                {
                    Id = 1,
                    Title = "Test Section"
                }
            }.AsQueryable();
            mockSectionSet.As<IQueryable<Section>>().Setup(m => m.Provider).Returns(sectionList.Provider);
            mockSectionSet.As<IQueryable<Section>>().Setup(m => m.Expression).Returns(sectionList.Expression);
            mockSectionSet.As<IQueryable<Section>>().Setup(m => m.ElementType).Returns(sectionList.ElementType);
            mockSectionSet.As<IQueryable<Section>>().Setup(m => m.GetEnumerator()).Returns(sectionList.GetEnumerator());

            mockLocationSet = new Mock<DbSet<Location>>();
            var locationList = new List<Location> 
            { 
                new Location 
                {
                    Id = 1,
                    City = "Albuquerque",
                    State = "NM",
                    Venue = "Church of Christ"
                }
            }.AsQueryable();
            mockLocationSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locationList.Provider);
            mockLocationSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locationList.Expression);
            mockLocationSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locationList.ElementType);
            mockLocationSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locationList.GetEnumerator());

            mockSermonContext.Setup(m => m.Sermons).Returns(mockSermonSet.Object); 
            mockSermonContext.Setup(m => m.Preachers).Returns(mockPreacherSet.Object);
            mockSermonContext.Setup(m => m.Medias).Returns(mockMediaSet.Object);
            mockSermonContext.Setup(m => m.Serieses).Returns(mockSeriesSet.Object);
            mockSermonContext.Setup(m => m.Sections).Returns(mockSectionSet.Object);
            mockSermonContext.Setup(m => m.Locations).Returns(mockLocationSet.Object); 

            //foreach (var sermon in _context.Sermons.OrderByDescending(s => s.Id).ToList())
            //{
            //    _repository.DeleteSermon(sermon.Id);
            //    _context.DeleteObject(order.SalesOrderDetails.First());
            //}

            //_repository.GetSermons().ToList().ForEach()                    

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

        //TODO: Test for Nathan A vs Nathan C issues

        [TestMethod]
        public void ItCanCreateSermonsFromFilenames()
        {
            MediaScan mediaScan = new MediaScan("Sermons", mockSermonContext.Object);
            mockSermonSet.Verify(m => m.Add(It.IsAny<Sermon>()), Times.Exactly(3));
            mockPreacherSet.Verify(m => m.Add(It.IsAny<Preacher>()), Times.Exactly(3));
            mockSermonContext.Verify(m => m.SaveChanges(), Times.Once());

            //Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "The Test Sermon"
            //                                                        && s.SermonPreacher.FirstName == "John"
            //                                                        && s.SermonPreacher.LastName == "Smith"
            //                                                        && s.SermonLocation.City == "Albuquerque"
            //                                                        && s.SermonLocation.State == "NM"
            //                                                        && s.SermonLocation.Venue == "Church of Christ"
            //                                                        && string.IsNullOrEmpty(s.Comment)
            //                                                        && string.IsNullOrEmpty(s.Passages)), "The Test Sermon not found");

            //Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "Luke 12a"
            //                                                        && s.SermonPreacher.FirstName == "Bob"), "Luke 12a not found");

            //Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is A Test Sermon"
            //                                                        && s.SermonPreacher.FirstName == "Bill"), "This is a Test Sermon not found");
        }

        [TestMethod]
        public void ItCanLookUpPreachersByFirstName()
        {
            //Arrange
            //_repository.InsertPreacher(new Preacher() { FirstName = "Bill", LastName = "Wyatt" });
            //_repository.Save();

            //Act
            MediaScan mediaScan = new MediaScan("Sermons", mockSermonContext.Object);

            //Assert
            //Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is A Test Sermon"
            //                                                        && s.SermonPreacher.FirstName == "Bill"
            //                                                        && s.SermonPreacher.LastName == "Wyatt"), "Preacher Bill Wyatt not found");
        }

        //[TestMethod]
        //public void ItAvoidsDuplicateSermons()
        //{
        //    //Arrange
        //    MediaScan mediaScan = new MediaScan("Sermons", _repository);

        //    //Act
        //    mediaScan = new MediaScan("Sermons", _repository);

        //    //Assert
        //    Assert.AreEqual(_repository.GetSermons().Count(s => s.Title == "This Is A Test Sermon"), 1, "Duplicate sermons found");
        //}

        //[TestMethod]
        //public void ItAvoidsDuplicatePreachers()
        //{
        //    //Arrange
        //    if (!_repository.GetPreachers().Any(p => p.FirstName == "Unit" && p.LastName == "Test"))
        //    {
        //        _repository.InsertPreacher(new Preacher() { FirstName = "Unit", LastName = "Test" });
        //        _repository.Save();
        //    }
        //    MediaScan mediaScan = new MediaScan("Sermons", _repository);

        //    var bill2Mp3 = File.Create(@"Sermons\Unit_This_Is_Another_Test_Sermon.mp3");
        //    bill2Mp3.Close();

        //    File.Delete(bill2Mp3.ToString());

        //    //Act
        //    mediaScan = new MediaScan("Sermons", _repository);

        //    //Assert
        //    Assert.IsTrue(_repository.GetSermons().Any(s => s.Title == "This Is Another Test Sermon"
        //                                                            && s.SermonPreacher.FirstName == "Unit"
        //                                                            && s.SermonPreacher.LastName == "Test"), "Preacher Unit Test not found");

        //    //TODO: WHEREYOUWERE MediaScan is creating duplicate preachers, regardless of what this says.
        //    Assert.AreEqual(_repository.GetPreachers().Count(p => p.FirstName == "Unit"), 1, "Duplicate preachers found");
        //}

        //[TestMethod]
        //public void ASermonCanBeDeleted()
        //{
        //    //Arrange
        //    Sermon sermon = new Sermon() { Title = "Sermon to Delete" };
        //    _repository.InsertSermon(sermon);
        //    //_repository.InsertPreacher(new Preacher() { FirstName = "John", LastName = "Smith" });
        //    _repository.Save();

        //    //Act
        //    _repository.DeleteSermon(sermon.Id);

        //    //Assert
        //    Assert.IsFalse(_repository.GetSermons().Any(s => s.Title == "Sermon to Delete"), "Sermon to Delete not found");
        //}
    }
}
