using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Adapters.Telegram.Tests.Shared;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Message = Telegram.Bot.Types.Message;
using PhotoSize = Telegram.Bot.Types.PhotoSize;

// ReSharper disable UseObjectOrCollectionInitializer

namespace Replikit.Adapters.Telegram.Tests.MessageService;

[UsesVerify]
public class SendTest
{
    [Fact]
    public Task ShouldSendSingleTextMessage()
    {
        async Task<object> Action(IAdapter adapter)
        {
            return await adapter.MessageService.SendAsync(12, "Hello World!");
        }

        var request = new UnderlyingRequest
        {
            Request = new SendMessageRequest(12, "Hello World!")
            {
                ParseMode = ParseMode.Html,
                ReplyToMessageId = 0
            },
            Response = new Message
            {
                MessageId = 42,
                Text = "Hello World!",
                Chat = new Chat { Id = 12 },
                From = TestData.TestBotUser
            }
        };

        return TestUtils.VerifyFeature(Action, request);
    }

    [Fact]
    public Task ShouldSendSingleTextMessageWithReply()
    {
        async Task<object> Action(IAdapter adapter)
        {
            var message = new OutMessage
            {
                Text = "Hello World!",
                Reply = 41
            };

            return await adapter.MessageService.SendAsync(12, message);
        }

        var request = new UnderlyingRequest
        {
            Request = new SendMessageRequest(12, "Hello World!")
            {
                ParseMode = ParseMode.Html,
                ReplyToMessageId = 41
            },
            Response = new Message
            {
                MessageId = 42,
                Text = "Hello World!",
                Chat = new Chat { Id = 12 },
                From = TestData.TestBotUser,
                ReplyToMessage = new Message
                {
                    MessageId = 41,
                    Chat = new Chat { Id = 12 },
                }
            }
        };

        return TestUtils.VerifyFeature(Action, request);
    }

    [Fact]
    public Task ShouldSendSingleTextMessageWithKeyboard()
    {
        async Task<object> Action(IAdapter adapter)
        {
            var message = new OutMessage
            {
                Text = "Hello World!",
                Keyboard =
                {
                    {
                        "Button 1",
                        "Button 2"
                    },

                    "Button 3"
                }
            };

            return await adapter.MessageService.SendAsync(12, message);
        }

        var request = new UnderlyingRequest
        {
            Request = new SendMessageRequest(12, "Hello World!")
            {
                ParseMode = ParseMode.Html,
                ReplyToMessageId = 0,
                ReplyMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Button 1"),
                        new KeyboardButton("Button 2")
                    },
                    new[]
                    {
                        new KeyboardButton("Button 3")
                    }
                })
            },
            Response = new Message
            {
                MessageId = 42,
                Text = "Hello World!",
                Chat = new Chat { Id = 12 },
                From = TestData.TestBotUser
            }
        };

        return TestUtils.VerifyFeature(Action, request);
    }

    [Fact]
    public Task ShouldSendSingleTextMessageWithInlineButtons()
    {
        async Task<object> Action(IAdapter adapter)
        {
            var message = new OutMessage
            {
                Text = "Hello World!",
                InlineButtons =
                {
                    {
                        new CallbackInlineButton("Button 1", "Callback 1"),
                        new CallbackInlineButton("Button 2", "Callback 2")
                    },
                    new LinkInlineButton("Button 3", new Uri("https://www.google.com"))
                }
            };

            return await adapter.MessageService.SendAsync(12, message);
        }

        var request = new UnderlyingRequest
        {
            Request = new SendMessageRequest(12, "Hello World!")
            {
                ParseMode = ParseMode.Html,
                ReplyToMessageId = 0,
                ReplyMarkup = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Button 1") { CallbackData = "Callback 1" },
                        new InlineKeyboardButton("Button 2") { CallbackData = "Callback 2" }
                    },
                    new[]
                    {
                        new InlineKeyboardButton("Button 3") { Url = "https://www.google.com" }
                    }
                })
            },
            Response = new Message
            {
                MessageId = 42,
                Text = "Hello World!",
                Chat = new Chat { Id = 12 },
                From = TestData.TestBotUser
            }
        };

        return TestUtils.VerifyFeature(Action, request);
    }

    [Fact]
    public Task ShouldSendPhotoAttachmentWithCaption()
    {
        async Task<object> Action(IAdapter adapter)
        {
            var message = new OutMessage
            {
                Attachments =
                {
                    OutAttachment.FromUrl(AttachmentType.Photo, new Uri("https://example.com/image.jpg"), "Caption")
                }
            };

            return await adapter.MessageService.SendAsync(12, message);
        }

        var request = new UnderlyingRequest
        {
            Request = new SendMediaGroupRequest(12, new[]
            {
                new InputMediaPhoto("https://example.com/image.jpg") { Caption = "Caption" }
            })
            {
                ReplyToMessageId = 0
            },
            Response = new[]
            {
                new Message
                {
                    MessageId = 42,
                    Chat = new Chat { Id = 12 },
                    From = TestData.TestBotUser,
                    Photo = new[]
                    {
                        new PhotoSize
                        {
                            FileUniqueId = "AttachmentId",
                            FileId = "AttachmentUploadId"
                        }
                    }
                }
            }
        };

        return TestUtils.VerifyFeature(Action, request);
    }
}
