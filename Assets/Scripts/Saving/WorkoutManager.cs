using UnityEngine;
using System.Collections.Generic;

public class WorkoutManager : MonoBehaviour
{
    public static WorkoutManager instance;
    private const string WorkoutsKey = "Workouts";
    private List<WorkoutData> allWorkouts = new List<WorkoutData>();

    private void Awake()
    {
        instance = this;
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
