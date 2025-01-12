using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TrainingType
{
    Strength,
    Cardio,
    Stretch
}

public class TrainingPart : MonoBehaviour
{
    public TrainingType trainingType;

    public TMP_Text name;
    public TMP_Text countXrepeats;

    public Image imgType;

    private void Start()
    {
        SetImage();
    }

    public void Init(string name, int count, int repeats, TrainingType trainingType)
    {
        this.name.text = name;
        this.countXrepeats.text = count.ToString()+" x "+repeats;
        this.trainingType = trainingType;
        SetImage();
    }

    private void SetImage()
    {
        switch(trainingType)
        {
            case TrainingType.Cardio:
                    imgType.sprite = Resources.Load<Sprite>("Cardio");
                break;
                case TrainingType.Stretch:
                imgType.sprite = Resources.Load<Sprite>("Stretch");
                break;
                case TrainingType.Strength:
                imgType.sprite = Resources.Load<Sprite>("Strength");
                break;
        }
    }
}
