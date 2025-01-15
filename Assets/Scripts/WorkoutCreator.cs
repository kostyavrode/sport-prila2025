using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UI.Dates;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutCreator : MonoBehaviour
{

    public GameObject workoutButtonPrefab;
    public WorkoutDetailsUI workoutDetailsUIPrefab;
    public Transform buttonContainer;

    public Transform container;

    void Start()
    {
        List<WorkoutData> workouts = CreateWorkouts();

        foreach (WorkoutData workout in workouts)
        {
            Debug.Log($"Workout: {workout.WorkoutName}, Date: {workout.date}");
            foreach (ExerciseData exercise in workout.Exercises)
            {
                Debug.Log($"- Exercise: {exercise.Name}, Sets: {exercise.Sets}, Reps: {exercise.Reps}, Type: {exercise.TrainingType}");
                
            }
        }
        GenerateWorkoutButtons(workouts);
    }

    List<WorkoutData> CreateWorkouts()
    {
        List<WorkoutData> workoutList = new List<WorkoutData>();

        // 1. Full Body Strength
        workoutList.Add(new WorkoutData(
            "Full Body Strength",
            new List<ExerciseData>
            {
                new ExerciseData("Squats", 3, 12, TrainingType.Strength),
                new ExerciseData("Push Ups", 3, 15, TrainingType.Strength),
                new ExerciseData("Deadlift", 3, 10, TrainingType.Strength)
            },
            new SerializableDate(System.DateTime.Today)
        ));

        // 2. Morning Cardio
        workoutList.Add(new WorkoutData(
            "Morning Cardio",
            new List<ExerciseData>
            {
                new ExerciseData("Running", 1, 20, TrainingType.Cardio),
                new ExerciseData("Jumping Jacks", 2, 30, TrainingType.Cardio),
                new ExerciseData("Burpees", 3, 10, TrainingType.Cardio)
            },
            new SerializableDate(System.DateTime.Today)
        ));

        // 3. Stretch and Relax
        workoutList.Add(new WorkoutData(
            "Stretch and Relax",
            new List<ExerciseData>
            {
                new ExerciseData("Hamstring Stretch", 2, 30, TrainingType.Stretch),
                new ExerciseData("Shoulder Stretch", 2, 20, TrainingType.Stretch),
                new ExerciseData("Child Pose", 3, 40, TrainingType.Stretch)
            },
            new SerializableDate(System.DateTime.Today)
        ));

        // 4. Core Strength
        workoutList.Add(new WorkoutData(
            "Core Strength",
            new List<ExerciseData>
            {
                new ExerciseData("Plank", 3, 60, TrainingType.Strength),
                new ExerciseData("Russian Twists", 3, 15, TrainingType.Strength),
                new ExerciseData("Leg Raises", 3, 12, TrainingType.Strength)
            },
            new SerializableDate(System.DateTime.Today)
        ));

        return workoutList;
    }
    public void GenerateWorkoutButtons(List<WorkoutData> workouts)
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject); // Очистка старых кнопок
        }

        foreach (var workout in workouts)
        {
            GameObject buttonObj = Instantiate(workoutButtonPrefab, buttonContainer);
            buttonObj.GetComponentInChildren<TMP_Text>().text = $"{workout.WorkoutName} ({workout.Exercises.Count} упражнений)";
            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnWorkoutButtonClicked(workout));
        }
    }
    private void OnWorkoutButtonClicked(WorkoutData workout)
    {
        // Открываем детали тренировки
        //FindObjectOfType<WorkoutDetailsUI>().ShowWorkoutDetails(workout);
        WorkoutDetailsUI workoutDetailsUI = Instantiate(workoutDetailsUIPrefab, container);
        workoutDetailsUI.ShowWorkoutDetails(workout);
        workoutDetailsUI.transform.localPosition = Vector3.zero;
        workoutDetailsUI.GetComponent<WindowManager>().OpenWindow();
    }
}
