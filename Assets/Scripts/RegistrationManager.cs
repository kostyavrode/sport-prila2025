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

    private Sprite selectedAvatar; // ��������� ����������� �������

    void Start()
    {
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
                                       && !string.IsNullOrEmpty(lastNameField.text);
    }

    void OpenAvatarSelection()
    {
        // �������� ������ �������. ���������� ����� ������ ������ ����� ��� �������
        // ����� ��� ������� ������������ ��������:
        selectedAvatar = Resources.Load<Sprite>("DefaultAvatar"); // �������� ������� �� ��������
        avatarPreview.sprite = selectedAvatar; // ���������� ������
        CheckFields(""); // ������������ �����
    }

    void RegisterUser()
    {
        string firstName = firstNameField.text;
        string lastName = lastNameField.text;

        // ���������� ������
        PlayerPrefs.SetString("FirstName", firstName);
        PlayerPrefs.SetString("LastName", lastName);
        PlayerPrefs.SetString("AvatarPath", "DefaultAvatar"); // ����� ���������� ���� ��� ������������� �������
        PlayerPrefs.Save();
    }
}
