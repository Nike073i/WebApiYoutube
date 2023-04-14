using System;
using FluentValidation;

namespace Notes.Application.Notes.Quiries.GetNoteDetails
{
    public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsQueryValidator()
        {
            RuleFor(query => query.UserId).NotEqual(Guid.Empty);
            RuleFor(query => query.Id).NotEqual(Guid.Empty);
        }
    }
}
