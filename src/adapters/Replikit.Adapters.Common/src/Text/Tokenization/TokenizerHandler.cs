using System.Text.RegularExpressions;
using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Adapters.Common.Text.Tokenization;

public delegate TextToken TokenizerHandler(GroupCollection groups);
