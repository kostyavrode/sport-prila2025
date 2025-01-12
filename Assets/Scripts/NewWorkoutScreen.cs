using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class NewWorkoutScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField workoutNameField;
    public TMP_InputField exerciseNameField;
    public TMP_InputField setsField;
    public TMP_InputField repsField;
    public TMP_Dropdown exerciseTypeDropdown;
    public Button addExerciseButton;
    public Button saveWorkoutButton;
    public Transform exerciseListParent;
    public TrainingPart exerciseItemPrefab;
    public TMP_Text selectedDateText;
    public Button openCalendarButton;
    private const string WorkoutsKey = "Workouts";
    private List<ExerciseData> exercises = new List<ExerciseData>();
    private string selectedDate = "";

    void Start()
    {
        addExerciseButton.onClick.AddListener(AddExercise);
        saveWorkoutButton.onClick.AddListener(SaveWorkout);
        //openCalendarButton.onClick.AddListener(OpenCalendar);

        saveWorkoutButton.interactable = false;
        UpdateSaveButtonState();
    }

    void AddExercise()
    {
        string exerciseName = exerciseNameField.text;
        int sets = int.Parse(setsField.text);
        int reps = int.Parse(repsField.text);
        TrainingType type = (TrainingType)exerciseTypeDropdown.value;

        if (string.IsNullOrEmpty(exerciseName) || sets <= 0 || reps <= 0)
        {
            Debug.Log("Введите корректные данные для упражнения!");
            return;
        }

        ExerciseData newExercise = new ExerciseData(exerciseName, sets, reps, type);
        exercises.Add(newExercise);

        // Добавляем визуальный элемент в список
        TrainingPart newItem = Instantiate(exerciseItemPrefab, exerciseListParent);
        //newItem.GetComponentInChildren<TMP_Text>().text = $"{exerciseName}: {sets} x {reps} ({type})";
        newItem.Init(exerciseName,sets,reps, type);

        // Очищаем поля
        exerciseNameField.text = "";
        setsField.text = "";
        repsField.text = "";

        UpdateSaveButtonState();
    }

    public void SaveWorkout()
    {
        WorkoutData workoutData = new WorkoutData(workoutNameField.text, exercises);
        WorkoutManager.instance.SaveWorkout(workoutData);
        ClearAllFields();
    }

    void OpenCalendar()
    {
        // Здесь вы можете вызвать ваш UI-календарь.
        // После выбора даты вызывайте метод SetSelectedDate.
        SetSelectedDate(System.DateTime.Now.ToString("yyyy-MM-dd")); // Пример, замените на реальную дату из календаря
    }

    public void SetSelectedDate(string date)
    {
        selectedDate = date;
        selectedDateText.text = $"Дата тренировки: {date}";
        UpdateSaveButtonState();
    }

    void UpdateSaveButtonState()
    {
        saveWorkoutButton.interactable = !string.IsNullOrEmpty(workoutNameField.text);
    }

    void ClearAllFields()
    {
        workoutNameField.text = "";
        selectedDate = "";
        exercises.Clear();

        foreach (Transform child in exerciseListParent)
        {
            Destroy(child.gameObject);
        }
    }
}
[System.Serializable]
public class WorkoutsList
{
    public List<WorkoutData> Workouts;

    public WorkoutsList()
    {
        Workouts = new List<WorkoutData>();
    }
}