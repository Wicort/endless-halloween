using DIContainer;
using Logic.Localization;
using UnityEngine;

namespace Infrastructure.PageProvider
{
    public class UIPage : MonoBehaviour
    {
        [SerializeField] private string _title;

        public string GetTitle()
        {
            string text = _title;

            return text;
        }
    }
}
