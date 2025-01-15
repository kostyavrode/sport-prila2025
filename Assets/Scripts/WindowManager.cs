using UnityEngine;
using DG.Tweening;

public class WindowManager : MonoBehaviour
{
    [Header("Animation Settings")]
    public float animationDuration = 0.5f; // Длительность анимации
    public Vector3 openScale = Vector3.one; // Конечный масштаб при открытии
    public Vector3 closedScale = Vector3.zero; // Начальный масштаб при закрытии

    private void Awake()
    {
        transform.localScale = closedScale; // Устанавливаем начальный масштаб
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true); // Активируем объект перед анимацией
        transform.DOScale(openScale, animationDuration).SetEase(Ease.OutBack); // Анимация масштаба
    }

    public void CloseWindow()
    {
        transform.DOScale(closedScale, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false); // Отключаем объект после завершения анимации
        });
    }
}
