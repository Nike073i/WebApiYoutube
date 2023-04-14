using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Quiries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsViewModel>
    {
        private readonly INotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<NoteDetailsViewModel> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            return _mapper.Map<NoteDetailsViewModel>(entity);
        }
    }
}
