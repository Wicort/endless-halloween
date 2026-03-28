using DIContainer;

namespace Logic.Localization
{
    public interface ILocalizationService : IBean
    {
        string GetLocalValue(string key);
    }
}