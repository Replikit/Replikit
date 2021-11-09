using System.Globalization;

namespace Replikit.Core.Localization;

public interface ILocalizer
{
    string Localize(Type localeType, string localeName, CultureInfo? cultureInfo = null);
}
