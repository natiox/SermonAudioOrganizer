namespace SermonAudioOrganizer.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SermonAudioOrganizer.Domain.SermonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SermonAudioOrganizer.Domain.SermonContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Sermons.AddOrUpdate(
                new Sermon()
                {
                    SermonPreacher = new Preacher() { FirstName = "John", LastName = "Smith" },
                    SermonLocation = new Location() { City = "Albuquerque", State = "NM", Venue = "Church of Christ on Vermont Street" },
                    Comment = "This is a comment.",
                    Passages = "Matt 5, Mark 4",
                    RecordingDate = new DateTime(2012, 2, 2),
                    SectionIndex = 1,
                    SeriesIndex = "1a",
                    SermonMedia = new List<Media>() { new Media() { Name = "Test.mp3", Type = MediaType.MP3 } },
                    SermonSection = new Section() { Title = "Test Section" },
                    SermonSeries = new Series() { Title = "Test Series" },
                    Title = "Test Sermon",
                    Topic = "Justice"
                }
                );
        }
    }
}
