using System;
using FluentValidation;

namespace Notes.Application.Notes.Quiries.GetNoteList
{
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidator()
        {
            RuleFor(query => query.UserId).NotEqual(Guid.Empty);
        }
    }
}
