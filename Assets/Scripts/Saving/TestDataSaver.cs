using UnityEngine;

public class TestDataSaver : MonoBehaviour
{
    private const string WorkoutsKey = "Workouts";

    void Start()
    {
        string sampleData = @"
        {
          ""Workouts"": [
            {
              ""WorkoutName"": ""Full Body Strength"",
              ""Exercises"": [
                {
                  ""Name"": ""Squats"",
                  ""Sets"": 3,
                  ""Reps"": 12,
                  ""TrainingType"": ""Strength""
                },
                {
                  ""Name"": ""Push Ups"",
                  ""Sets"": 3,
                  ""Reps"": 15,
                  ""TrainingType"": ""Strength""
                },
                {
                  ""Name"": ""Plank"",
                  ""Sets"": 3,
                  ""Reps"": 60,
                  ""TrainingType"": ""Strength""
                }
              ]
            },
            {
              ""WorkoutName"": ""Morning Cardio"",
              ""Exercises"": [
                {
                  ""Name"": ""Running"",
                  ""Sets"": 1,
                  ""Reps"": 20,
                  ""TrainingType"": ""Cardio""
                },
                {
                  ""Name"": ""Jumping Jacks"",
                  ""Sets"": 2,
                  ""Reps"": 30,
                  ""TrainingType"": ""Cardio""
                },
                {
                  ""Name"": ""Burpees"",
                  ""Sets"": 3,
                  ""Reps"": 10,
                  ""TrainingType"": ""Cardio""
                }
              ]
            }
          ]
        }";

        PlayerPrefs.SetString(WorkoutsKey, sampleData);
        PlayerPrefs.Save();

        Debug.Log("Sample workouts saved!");
    }
}
