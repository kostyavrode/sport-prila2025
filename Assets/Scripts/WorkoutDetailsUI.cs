using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class WorkoutDetailsUI : MonoBehaviour
{
    public GameObject trainingPartPrefab;
    public Transform trainingPartsContainer;

    public TMP_Text trainingName;
    public TMP_Text trainingDate;

    public Image execiseImage;
    public TMP_Text exerciseName;
    public TMP_Text exerciseSetsXRepeats;
    public TMP_Text progress;

    private WorkoutData workoutD;

    public GameObject endTraining;
    public GameObject trainingObject;

    private int templ;
    private ExerciseData[] exerciseData;

    public void ShowWorkoutDetails(WorkoutData workout)
    {
        workoutD = workout;
        List<ExerciseData> exs= new List<ExerciseData>();
        foreach (Transform child in trainingPartsContainer)
        {
            Destroy(child.gameObject); // Очистка предыдущих данных
        }

        trainingDate.text=workout.date.ToString().Split(' ')[0];
        trainingName.text = workout.WorkoutName;

        foreach (var exercise in workout.Exercises)
        {
            exs.Add(exercise);
            GameObject partObj = Instantiate(trainingPartPrefab, trainingPartsContainer);
            TrainingPart part = partObj.GetComponent<TrainingPart>();
            part.Init(exercise.Name, exercise.Sets, exercise.Reps, exercise.TrainingType);
            
        }
        exerciseData = exs.ToArray();
    }

    public void StartTraining()
    {
        if (templ < workoutD.Exercises.Count)
        {
            
            progress.text = (templ+1).ToString() + "|"+ workoutD.Exercises.Count;
            switch(exerciseData[templ].TrainingType)
            {
                case TrainingType.Cardio:
                    execiseImage.sprite = Resources.Load<Sprite>("Cardio");
                    break;
                case TrainingType.Stretch:
                    execiseImage.sprite = Resources.Load<Sprite>("Stretch");
                    break;
                case TrainingType.Strength:
                    execiseImage.sprite = Resources.Load<Sprite>("Strength");
                    break;
            }
            exerciseSetsXRepeats.text = exerciseData[templ].Sets.ToString();
            exerciseName.text= exerciseData[templ].Name.ToString();
            templ = templ + 1;
        }
        else
        {
            endTraining.gameObject.GetComponent<WindowManager>().OpenWindow();
            trainingObject.gameObject.GetComponent<WindowManager>().CloseWindow();
        }
    }
    public void CloseButton()
    {
        GetComponent<WindowManager>().CloseWindow();
        StartCoroutine(Wait05fSec(this.gameObject));
    }
    private IEnumerator Wait05fSec(GameObject g)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(g);
    }
}
