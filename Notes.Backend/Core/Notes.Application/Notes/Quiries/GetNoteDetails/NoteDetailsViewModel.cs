using System;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Quiries.GetNoteDetails
{
    public class NoteDetailsViewModel : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
