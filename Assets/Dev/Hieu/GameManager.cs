using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject scorePanel;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;
    public TextMeshProUGUI mistakeText;
    public TextMeshProUGUI timeText;
    public TimerScript timerScript;

    public int mistakeCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        scorePanel.SetActive(false);
    }

    public void AddMistake()
    {
        mistakeCount++;
    }

    public void ShowScore()
    {
        int correctCount = 0;
        int wrongCount = 0;

        var draggables = FindObjectsOfType<DraggableObject>();

        foreach (var draggable in draggables)
        {
            NumberSlot slot = draggable.currentConnectedSlot;
            bool isCorrectNow = false;

            if (slot != null)
            {
                isCorrectNow = (draggable.objectCount == slot.numberSlot);
            }

            if (isCorrectNow)
                correctCount++;
            else
                wrongCount++;

            Debug.Log($"Obj {draggable.name}: {(isCorrectNow ? "ĐÚNG" : "SAI")}, objCount: {draggable.objectCount}, slot: {(slot != null ? slot.numberSlot.ToString() : "None")}");
        }

        timerScript.StopTimer();

        Debug.Log($"Tổng ĐÚNG: {correctCount}, SAI: {wrongCount}, Số lần mắc lỗi: {mistakeCount}, Thời gian: {timerScript.GetFormattedTime()} ");

        correctText.text = $" {correctCount}";
        wrongText.text = $" {wrongCount}";
        mistakeText.text = $" {mistakeCount}";
        timeText.text = $" {timerScript.GetFormattedTime()}";

        scorePanel.SetActive(true);

        ClearAllLines();
    }

    public void ClearAllLines()
    {
        var draggables = FindObjectsOfType<DraggableObject>();
        foreach (var draggable in draggables)
        {
            draggable.ClearLineRenderer();
        }
    }

    public void ResetGame()
    {
        mistakeCount = 0;
        var draggables = FindObjectsOfType<DraggableObject>();
        foreach (var draggable in draggables)
        {
            draggable.ClearLineRenderer();
            draggable.transform.position = draggable.startPoint.position; 
            draggable.currentConnectedSlot = null;
        }

        scorePanel.SetActive(false);
      
    }
}
