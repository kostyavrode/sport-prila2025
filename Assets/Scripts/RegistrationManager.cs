using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{
    public TMP_InputField firstNameField;
    public TMP_InputField lastNameField;
    public Button selectAvatarButton;
    public Image avatarPreview;
    public Button registerButton;

    private Sprite selectedAvatar; // Выбранное изображение аватара

    void Start()
    {
        registerButton.interactable = false; // Кнопка регистрации недоступна
        firstNameField.onValueChanged.AddListener(CheckFields);
        lastNameField.onValueChanged.AddListener(CheckFields);
        selectAvatarButton.onClick.AddListener(OpenAvatarSelection);
        registerButton.onClick.AddListener(RegisterUser);
    }

    void CheckFields(string _)
    {
        // Проверка: заполнены ли поля и выбран ли аватар
        registerButton.interactable = !string.IsNullOrEmpty(firstNameField.text)
                                       && !string.IsNullOrEmpty(lastNameField.text);
    }

    void OpenAvatarSelection()
    {
        // Имитация выбора аватара. Реализуйте через диалог выбора файла или галерею
        // Здесь для примера используется заглушка:
        selectedAvatar = Resources.Load<Sprite>("DefaultAvatar"); // Загрузка аватара из ресурсов
        avatarPreview.sprite = selectedAvatar; // Обновление превью
        CheckFields(""); // Перепроверка полей
    }

    void RegisterUser()
    {
        string firstName = firstNameField.text;
        string lastName = lastNameField.text;

        // Сохранение данных
        PlayerPrefs.SetString("FirstName", firstName);
        PlayerPrefs.SetString("LastName", lastName);
        PlayerPrefs.SetString("AvatarPath", "DefaultAvatar"); // Здесь указывайте путь или идентификатор аватара
        PlayerPrefs.Save();
    }
}
