using AutoFixture;
using FluentAssertions;
using privatext.Database;
using privatext.Database.Models;

namespace privatext.DatabaseTest;

public class RepoTest : BaseDataContextTest
{
    [Fact]
    public async Task TestInsertMessages()
    {
        // Arrange
        var fixture = new Fixture();
        var postId = 1;
        var messages = fixture.Build<Message>()
            .With(s => s.Id, () => postId++).CreateMany(50);

        // Act
        using (var context = new DatabaseContext(_connection))
        {
            context.Messages.AddRange(messages);
            await context.SaveChangesAsync();
        }

        // Assert
        using (var context = new DatabaseContext(_connection))
        {
            context.Messages.Count().Should().Be(50);
        }
    }
}