using FluentAssertions;
using privatext.Common.DTO;
using privatext.Database;
using privatext.Database.Models;
using privatext.DatabaseTest;
using privatext.Services;

namespace privatext.Test;

public class MessageServiceTest : BaseDataContextTest
{
    private MessageService messageService;
    public MessageServiceTest()
    {
        messageService = new MessageService(new DatabaseContext(_connection));
    }

    [Fact]
    public async Task GetMessageOK()
    {
        // Arrange
        var content = "#fgaje;jf'pvcxi123";
        var dateCreated = DateTime.Now;
        using (var context = new DatabaseContext(_connection))
        {
            var model = new Message
            {
                Id = 1,
                Content = "#fgaje;jf'pvcxi123",
                DateCreated = dateCreated,
            };

            await context.AddAsync(model);
            await context.SaveChangesAsync();
        }

        // Act
        var result = messageService.GetMessage(1);

        // Assert
        result.Content.Should().Be(content);
        result.DateCreated.Should().Be(dateCreated);
    }

    [Fact]
    public async Task DeleteMessageOK()
    {
        // Arrange
        using (var context = new DatabaseContext(_connection))
        {
            var model = new Message
            {
                Id = 1,
                Content = "#fgaje;jf'pvcxi123",
            };

            await context.AddAsync(model);
            await context.SaveChangesAsync();
        }

        // Act
        await messageService.DeleteMessage(1);

        // Assert
        using (var context = new DatabaseContext(_connection))
        {
            context.Messages.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task CreateMessageOK()
    {
        // Arrange
        var content = "qjg;p8943j";
        var dto = new MessageDTO
        {
            Content = content,
        };

        // Act
        await messageService.CreateMessage(dto);

        // Assert
        using (var context = new DatabaseContext(_connection))
        {
            context.Messages.First().Content.Should().Be(content);
        }
    }
}
