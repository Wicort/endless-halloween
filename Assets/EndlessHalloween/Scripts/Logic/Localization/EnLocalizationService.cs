using System.Collections.Generic;

namespace Logic.Localization
{
    public class EnLocalizationService : ILocalizationService
    {
        private bool isDefault;
        private Dictionary<string, string> LocalValues = new Dictionary<string, string>
        {
            { "key" , "value"},
        };

        public EnLocalizationService(bool isDefault = false)
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
            if ( LocalValues.ContainsKey(key))
            {
                return LocalValues[key];
            }

            return key;
        }
    }
}
