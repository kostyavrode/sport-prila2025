using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonsController : MonoBehaviour
{
    [SerializeField] private Image mainButton;
    [SerializeField] private Image addTrainingButton;
    [SerializeField] private Image starterTrainigsButton;
    [SerializeField] private Image profileButton;

    [SerializeField] private Transform mainWindow;
    [SerializeField] private Transform addTrainingWindow;
    [SerializeField] private Transform starterTrainingsWindow;
    [SerializeField] private Transform profileWindow;

    private void Update()
    {
        if (mainWindow.gameObject.activeSelf)
        {
            SetImageAlpha(1, mainButton);
            SetImageAlpha(0.7f, addTrainingButton);
            SetImageAlpha(0.7f, starterTrainigsButton);
            SetImageAlpha(0.7f, profileButton);
        }
        else if (addTrainingWindow.gameObject.activeSelf)
        {
            SetImageAlpha(0.7f, mainButton);
            SetImageAlpha(1, addTrainingButton);
            SetImageAlpha(0.7f, starterTrainigsButton);
            SetImageAlpha(0.7f, profileButton);
        }
        else if (starterTrainigsButton.gameObject.activeSelf)
        {
            SetImageAlpha(0.7f, mainButton);
            SetImageAlpha(0.7f, addTrainingButton);
            SetImageAlpha(1, starterTrainigsButton);
            SetImageAlpha(0.7f, profileButton);
        }
        else
        {
            SetImageAlpha(0.7f, mainButton);
            SetImageAlpha(0.7f, addTrainingButton);
            SetImageAlpha(0.7f, starterTrainigsButton);
            SetImageAlpha(1, profileButton);
        }
    }
    public void SetImageAlpha(float alpha, Image targetImage)
    {
        if (targetImage != null)
        {
            Color currentColor = targetImage.color;
            currentColor.a = Mathf.Clamp01(alpha);
            targetImage.color = currentColor;
        }
    }
}
