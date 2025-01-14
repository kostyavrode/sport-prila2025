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

    private string avatarPath; // ���� � ������������ �������

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
        registerButton.interactable = false; // ������ ����������� ����������
        firstNameField.onValueChanged.AddListener(CheckFields);
        lastNameField.onValueChanged.AddListener(CheckFields);
        selectAvatarButton.onClick.AddListener(OpenAvatarSelection);
        registerButton.onClick.AddListener(RegisterUser);
    }

    void CheckFields(string _)
    {
        // ��������: ��������� �� ���� � ������ �� ������
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
                // ��������� �������� � ������ � ��������
                Texture2D texture = NativeGallery.LoadImageAtPath(path, -1, false);
                if (texture != null)
                {
                    // ������ ������ �� ��������
                    Sprite createdSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // ���������� ������ � Image
                    avatarPreview.sprite = createdSprite;

                    // ��������� ���� ��� ������������ �������������
                    avatarPath = SaveAvatarLocally(texture);
                    CheckFields(""); // ���������, ��������� �� ��� ����
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
        // ���������, ��� �������� ��������
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

        // ��������� ������ ������������
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
        // �������� ���������� ���� �� PlayerPrefs
        string avatarPath = PlayerPrefs.GetString("AvatarPath", "");

        if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
        {
            // ��������� ����������� ��� ��������
            byte[] imageData = File.ReadAllBytes(avatarPath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageData))
            {
                // ������ ������ �� ��������
                Sprite avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // ����������� ������ Image
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
