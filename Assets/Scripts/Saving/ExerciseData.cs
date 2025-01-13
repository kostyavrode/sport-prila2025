using System;
using System.Collections.Generic;
using UI.Dates;

[System.Serializable]
public class ExerciseData
{
    public string Name;
    public int Sets;
    public int Reps;
    public TrainingType TrainingType;

    public ExerciseData(string name, int sets, int reps, TrainingType type)
    {
        Name = name;
        Sets = sets;
        Reps = reps;
        TrainingType = type;
    }
}

[System.Serializable]
public class WorkoutData
{
    public string WorkoutName;
    public SerializableDate date;
    public List<ExerciseData> Exercises = new List<ExerciseData>();


    public WorkoutData(string workoutName, List<ExerciseData> exercise, SerializableDate serializableDate)
    {

        WorkoutName = workoutName;
        foreach(ExerciseData ex in exercise)
        {
            Exercises.Add(ex);
        }
        date = serializableDate;
    }
}
