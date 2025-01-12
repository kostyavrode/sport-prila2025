using UnityEngine;

public class WorkoutDetailsUI : MonoBehaviour
{
    public GameObject trainingPartPrefab;
    public Transform trainingPartsContainer;

    public void ShowWorkoutDetails(WorkoutData workout)
    {
        foreach (Transform child in trainingPartsContainer)
        {
            Destroy(child.gameObject); // Очистка предыдущих данных
        }

        foreach (var exercise in workout.Exercises)
        {
            GameObject partObj = Instantiate(trainingPartPrefab, trainingPartsContainer);
            TrainingPart part = partObj.GetComponent<TrainingPart>();
            part.Init(exercise.Name, exercise.Sets, exercise.Reps, exercise.TrainingType);
        }
    }
    public void CloseButton()
    {
        Destroy(gameObject);
    }
}
