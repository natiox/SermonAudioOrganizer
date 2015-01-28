using System;
using System.Linq;
namespace SermonAudioOrganizer.Domain
{
    interface ISermonContext
    {
        IQueryable<T> Query<T>() where T : class;
    }
}
