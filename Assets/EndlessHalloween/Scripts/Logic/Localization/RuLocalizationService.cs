using System.Collections.Generic;

namespace Logic.Localization
{
    public class RuLocalizationService : ILocalizationService
    {
        private bool isDefault;
        private Dictionary<string, string> LocalValues = new Dictionary<string, string>
        {
            { "Welcome Screen" , "Экран приветствия"},
            { "Shop" , "Магазин"},
            { "Customize" , "Кастомизация"},
            { "Main" , "Основная"},
            { "Events" , "События"},
        };

        public RuLocalizationService(bool isDefault = false)
        {
            this.isDefault = isDefault;
        }

        public string GetLocalValue(string key)
        {
            if (isDefault)
            {
                return key;
            }

            return GetValByKey(key);
        }

        private string GetValByKey(string key)
        {
            if (LocalValues.ContainsKey(key))
            {
                return LocalValues[key];
            }

            return key;
        }
    }
}
