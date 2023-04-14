using System;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Quiries.GetNoteList
{
    public class NoteLookupDto : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
