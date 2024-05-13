using AutoFixture;
using privatext.Database;
using privatext.Database.Models;

namespace privatext.Test;

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
            Assert.True(context.Messages.Count() == 50);
        }
    }
}