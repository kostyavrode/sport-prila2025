using UnityEngine;
using System.Collections.Generic;
using UI.Dates;
using System;
using UnityEngine.UI;

public class WorkoutManager : MonoBehaviour
{
    public static WorkoutManager instance;

    public GameObject targetObject;
    private bool wasActive;

    public DatePicker_DayButton[] dayButtons;
    public Transform rowsContainer;

    private const string WorkoutsKey = "Workouts";
    private List<WorkoutData> allWorkouts = new List<WorkoutData>();

    private void Awake()
    {
        instance = this;
        wasActive = targetObject.activeSelf;
    }
    void Update()
    {
        // Проверяем, изменилось ли состояние на "включено"
        if (!wasActive && targetObject.activeSelf)
        {
            allWorkouts.Clear();
            LoadWorkouts();
        }

        // Обновляем предыдущее состояние
        wasActive = targetObject.activeSelf;
    }

    public void SaveWorkout(WorkoutData workout)
    {
        allWorkouts.Add(workout);
        string json = JsonUtility.ToJson(new WorkoutCollection(allWorkouts));
        PlayerPrefs.SetString(WorkoutsKey, json);
        PlayerPrefs.Save();
    }

    public void LoadWorkouts()
    {
        if (PlayerPrefs.HasKey(WorkoutsKey))
        {
            string json = PlayerPrefs.GetString(WorkoutsKey);
            allWorkouts = JsonUtility.FromJson<WorkoutCollection>(json).Workouts;
        }
        else
        {
            allWorkouts = new List<WorkoutData>();
        }

        dayButtons = rowsContainer.GetComponentsInChildren<DatePicker_DayButton>();

        CompareDateArrays(dayButtons, allWorkouts.ToArray());
    }

    public void CompareDateArrays(DatePicker_DayButton[] array1, WorkoutData[] array2)
    {
        HashSet<DatePicker_DayButton> matchingDates = new HashSet<DatePicker_DayButton>();
        
        foreach (DatePicker_DayButton date1 in array1)
        {
            foreach (WorkoutData date2 in array2)
            {
                Debug.Log(date1.Date + " || " + date2.date);
                // Сравниваем только дату (без времени)
                if (date1.Date == date2.date)
                {
                    matchingDates.Add(date1);
                    Debug.Log(date1.Date + " || " + date2.date);
                }
            }
        }

        if (matchingDates.Count > 0)
        {
            foreach (DatePicker_DayButton match in matchingDates)
            {
                Debug.Log($"Matching date found: {match:dd.MM.yyyy}");
                match.GetComponentInChildren<Image>().color = Color.red;
            }
        }
        else
        {
            Debug.Log("No matching dates found.");
        }
    }

    public List<WorkoutData> GetWorkouts()
    {
        return allWorkouts;
    }

    [System.Serializable]
    public class WorkoutCollection
    {
        public List<WorkoutData> Workouts;

        public WorkoutCollection(List<WorkoutData> workouts)
        {
            Workouts = workouts;
        }
    }
}
