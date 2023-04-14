using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Application.Notes.Quiries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext Context;
        private readonly IMapper Mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

            var result = await handler.Handle(
                new GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserBId,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NoteDetailsViewModel>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}
