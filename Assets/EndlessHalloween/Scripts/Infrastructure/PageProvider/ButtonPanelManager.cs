using Infrastructure.PageProvider;
using Logic.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelManager : MonoBehaviour
{
    public GameObject buttonPrefab; // Префаб кнопки
    public UIPage[] panelPrefabs; // Массив префабов панелей
    public Transform buttonPanel; // Панель, на которой будут располагаться кнопки
    public int defaultButtonIndex = 0;


    private GameObject selectedButton; // Текущая выбранная кнопка
    private Vector2 defaultButtonSize = new Vector2(75, 75); // Размер кнопки по умолчанию
    private Vector2 selectedButtonSize = new Vector2(100, 100); // Размер выбранной кнопки

    void Start()
    {
        // Создаем кнопки для каждого префаба панели
        for (int i = 0; i < panelPrefabs.Length; i++)
        {
            CreateButton(panelPrefabs[i], i);
        }

        // Центрируем кнопки на панели
        CenterButtons();

        // Нажимаем кнопку по умолчанию
        if (defaultButtonIndex >= 0 && defaultButtonIndex < buttonPanel.childCount)
        {
            OnButtonClick(buttonPanel.GetChild(defaultButtonIndex).gameObject, panelPrefabs[defaultButtonIndex]);
        }
    }

    void CreateButton(UIPage panelPrefab, int index)
    {
        // Создаем кнопку из префаба
        GameObject button = Instantiate(buttonPrefab, buttonPanel);

        // Назначаем действие при нажатии на кнопку
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(button, panelPrefab));

        // Устанавливаем текст кнопки (можно изменить на нужный)
        Text buttonText = button.GetComponentInChildren<Text>();
        buttonText.text = panelPrefab.GetTitle();

        if (button.TryGetComponent(out TextLocalization localization))
        {
            localization.SetKey(buttonText.text);
            localization.Translate();
        }

        // Устанавливаем размер кнопки по умолчанию
        button.GetComponent<RectTransform>().sizeDelta = defaultButtonSize;
    }

    void CenterButtons()
    {
        // Получаем компонент HorizontalLayoutGroup для центрирования кнопок
        HorizontalLayoutGroup layoutGroup = buttonPanel.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        //layoutGroup.spacing = 10f; // Расстояние между кнопками
    }

    void OnButtonClick(GameObject clickedButton, UIPage panelPrefab)
    {
        // Если есть выбранная кнопка, сбрасываем её размер
        if (selectedButton != null)
        {
            selectedButton.GetComponent<RectTransform>().sizeDelta = defaultButtonSize;
        }

        // Устанавливаем новую выбранную кнопку
        selectedButton = clickedButton;

        // Увеличиваем размер выбранной кнопки
        selectedButton.GetComponent<RectTransform>().sizeDelta = selectedButtonSize;

        // Отображаем панель
        ShowPanel(panelPrefab);
    }

    void ShowPanel(UIPage panelPrefab)
    {
        // Удаляем предыдущую панель, если она есть
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Panel"))
            {
                Destroy(child.gameObject);
            }
        }

        // Создаем новую панель из префаба
        Instantiate(panelPrefab, transform);
    }
}
