using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Quiries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListViewModel>
    {
        private readonly INotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(INotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<NoteListViewModel> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _context.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NoteListViewModel
            {
                Notes = notesQuery
            };
        }
    }
}
