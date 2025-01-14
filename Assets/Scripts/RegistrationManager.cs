using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class RegistrationManager : MonoBehaviour
{
    public TMP_InputField firstNameField;
    public TMP_InputField lastNameField;
    public Button selectAvatarButton;
    public Image avatarPreview;
    public Button registerButton;

    public TMP_Text playerName;
    public TMP_Text playerSurname;
    public Image avatar;

    private string avatarPath; // Путь к сохраненному аватару

    public GameObject onBoarding;

    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstName"))
        {
            onBoarding.SetActive(true);
        }
        else
        {
            LoadUser();
        }
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
                                       && !string.IsNullOrEmpty(lastNameField.text)
                                       && !string.IsNullOrEmpty(avatarPath);
    }

    void OpenAvatarSelection()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Загружаем текстуру и делаем её читаемой
                Texture2D texture = NativeGallery.LoadImageAtPath(path, -1, false);
                if (texture != null)
                {
                    // Создаём спрайт из текстуры
                    Sprite createdSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Отображаем спрайт в Image
                    avatarPreview.sprite = createdSprite;

                    // Сохраняем путь для последующего использования
                    avatarPath = SaveAvatarLocally(texture);
                    CheckFields(""); // Проверяем, заполнены ли все поля
                }
                else
                {
                    Debug.LogError("Failed to load texture from path: " + path);
                }
            }
        }, "Select an Avatar", "image/*");

        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.LogWarning("Permission to access gallery was denied.");
        }
    }

    string SaveAvatarLocally(Texture2D texture)
    {
        // Проверяем, что текстура читаемая
        if (texture.isReadable)
        {
            string savePath = Path.Combine(Application.persistentDataPath, "avatar.png");
            File.WriteAllBytes(savePath, texture.EncodeToPNG());
            return savePath;
        }
        else
        {
            Debug.LogError("Texture is not readable and cannot be saved.");
            return null;
        }
    }


    void RegisterUser()
    {
        string firstName = firstNameField.text;
        string lastName = lastNameField.text;

        // Сохраняем данные пользователя
        PlayerPrefs.SetString("FirstName", firstName);
        PlayerPrefs.SetString("LastName", lastName);
        PlayerPrefs.SetString("AvatarPath", avatarPath);
        PlayerPrefs.Save();

        Debug.Log($"User Registered: {firstName} {lastName}, Avatar Path: {avatarPath}");
        LoadUser();
    }

    public void LoadUser()
    {
        playerName.text = PlayerPrefs.GetString("FirstName");
        playerSurname.text = PlayerPrefs.GetString("LastName");
        LoadAvatar();
    }
    public void LoadAvatar()
    {
        // Получаем сохранённый путь из PlayerPrefs
        string avatarPath = PlayerPrefs.GetString("AvatarPath", "");

        if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
        {
            // Загружаем изображение как текстуру
            byte[] imageData = File.ReadAllBytes(avatarPath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                // Создаём спрайт из текстуры
                Sprite avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // Присваиваем спрайт Image
                avatar.sprite = avatarSprite;
            }
            else
            {
                Debug.LogError("Failed to load image data.");
            }
        }
        else
        {
            Debug.LogWarning("Avatar path is empty or file does not exist.");
        }
    }
}
