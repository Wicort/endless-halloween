using DIContainer;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Localization
{
    public class TextLocalization : MonoBehaviour
    {
        [SerializeField] private Text _textField;
        [SerializeField] private string _key;

        private ILocalizationService _localizationService;

        private void Awake()
        {
            _localizationService = DI.Container.Single<ILocalizationService>();

            if (_textField == null) _textField = GetComponent<Text>();

            if (_key == null || _key == "") _key = _textField.text;
        }

        private void Start()
        {
            Translate();
        }

        private void OnValidate()
        {
            if (_textField == null)
            {
                _textField = GetComponent<Text>();

                if (_textField == null) return;

                if (_key == null || _key == "") _key = _textField.text;
            }
        }

        public void Translate()
        {
            if (_textField == null) return;

            _textField.text = _localizationService.GetLocalValue(_key);
        }

        public void SetKey(string value)
        {
            _key = value;
        }
        
    }
}
