using System;
using MediatR;

namespace Notes.Application.Notes.Quiries.GetNoteList
{
    public class GetNoteListQuery : IRequest<NoteListViewModel>
    {
        public Guid UserId { get; set; }
    }
}
