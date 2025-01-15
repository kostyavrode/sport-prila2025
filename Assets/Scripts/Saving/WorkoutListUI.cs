using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class WorkoutListUI : MonoBehaviour
{

    public static WorkoutListUI instance;
    public GameObject workoutButtonPrefab;
    public WorkoutDetailsUI workoutDetailsUIPrefab;
    public Transform buttonContainer;

    public Transform container;

    private WorkoutManager workoutManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        workoutManager = FindObjectOfType<WorkoutManager>();
        workoutManager.LoadWorkouts();
        GenerateWorkoutButtons(workoutManager.GetWorkouts());
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
    public void CheckButtonsForDate(DateTime date)
    {
        try
        {
            //Transform[] childs = buttonContainer.GetComponentsInChildren<Transform>();
            //foreach (Transform child in childs)
            //{
            //    Destroy(child.gameObject);
            //}
            foreach (Transform child in buttonContainer)
            {
                Destroy(child.gameObject); // Очистка старых кнопок
            }
        }
        catch { }

        List<WorkoutData> workouts=workoutManager.GetWorkouts();
        foreach (var workout in workouts)
        {
            if (workout.date == date)
            {
                GameObject buttonObj = Instantiate(workoutButtonPrefab, buttonContainer);
                buttonObj.GetComponentInChildren<TMP_Text>().text = $"{workout.WorkoutName} ({workout.Exercises.Count} упражнений)";
                buttonObj.GetComponent<Button>().onClick.AddListener(() => OnWorkoutButtonClicked(workout));
            }
        }
    }
    private void OnWorkoutButtonClicked(WorkoutData workout)
    {
        // Открываем детали тренировки
        //FindObjectOfType<WorkoutDetailsUI>().ShowWorkoutDetails(workout);
        WorkoutDetailsUI workoutDetailsUI = Instantiate(workoutDetailsUIPrefab, container);
        workoutDetailsUI.ShowWorkoutDetails(workout);
        workoutDetailsUI.transform.localPosition=Vector3.zero;
        workoutDetailsUI.GetComponent<WindowManager>().OpenWindow();
    }
}
