using System.ComponentModel.DataAnnotations;

namespace Replikit.Adapters.Telegram;

public class TelegramAdapterOptions
{
    [Required]
    public string Token { get; set; } = null!;
}
