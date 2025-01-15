using UnityEngine;
using DG.Tweening;

public class WindowManager : MonoBehaviour
{
    [Header("Animation Settings")]
    public float animationDuration = 0.5f; // ������������ ��������
    public Vector3 openScale = Vector3.one; // �������� ������� ��� ��������
    public Vector3 closedScale = Vector3.zero; // ��������� ������� ��� ��������

    private void Awake()
    {
        transform.localScale = closedScale; // ������������� ��������� �������
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true); // ���������� ������ ����� ���������
        transform.DOScale(openScale, animationDuration).SetEase(Ease.OutBack); // �������� ��������
    }

    public void CloseWindow()
    {
        transform.DOScale(closedScale, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false); // ��������� ������ ����� ���������� ��������
        });
    }
}
