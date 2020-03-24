using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChatApp.Test
{
    public class MessageRepositoryTest
    {
        private readonly Mock<IMessageRepository> _messageRepositoryMock;

        public MessageRepositoryTest()
        {
            _messageRepositoryMock = new Mock<IMessageRepository>();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(30)]
        [InlineData(50)]
        public async System.Threading.Tasks.Task GetAll_Should_Return_Messages_SuccesfullyAsync(int count)
        {
            var messages = new List<MessageDto>();

            for (int i = 0; i < count; i++)
            {
                messages.Add(new MessageDto 
                { 
                    Text = "Test",
                    SentFrom = "",
                    CreatedOn = DateTime.UtcNow,
                    MessageID = i
                });
            }

            _messageRepositoryMock.Setup(m => m.GetAll(It.Is<int>(x => x == count))).ReturnsAsync(messages);

            var resultMessages = (await _messageRepositoryMock.Object.GetAll(count)).ToList();

            Assert.NotNull(messages);
            Assert.Equal(resultMessages.Count(), count);
        }
    }
}
