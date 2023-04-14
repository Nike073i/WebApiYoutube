using System;
using MediatR;

namespace Notes.Application.Notes.Quiries.GetNoteDetails
{
    public class GetNoteDetailsQuery : IRequest<NoteDetailsViewModel>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
