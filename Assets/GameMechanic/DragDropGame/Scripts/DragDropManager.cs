using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameMechanic.DragDrop{
    public class DragDropManager : MonoBehaviour
    {
        private Button btnComplete;
        private TMP_Text timerText;

        public int correctAnswers = 0;
        public int wrongAnswers;
        public int wrongAttemps = 0;
        private int totalQuestions;

        private float startTime = 0;
        private float time = 0;
        private bool isStarted = false;
        GameObject[] dropZones;

        [Header("Random số")]
        [Tooltip("Bật để các dropzone random số yêu cầu, tắt để tự điều chỉnh giá trị từng dropzone")]
        // Để là false để tự điều chỉnh số của các dropzone
        public bool isRandom = true;

        // Start is called before the first frame update
        void Start()
        {
            // Lấy btn hoàn thành
            btnComplete = GameObject.FindGameObjectWithTag("BtnComplete").GetComponent<Button>();

            dropZones = GameObject.FindGameObjectsWithTag("DropZone");

            // Lấy text thời gian
            timerText = GameObject.FindGameObjectWithTag("TextTimer").GetComponent<TMP_Text>();

            btnComplete.onClick.AddListener(Submit);

            totalQuestions = dropZones.Length;

            StartTimer();
        }

        // Update is called once per frame
        void Update()
        {
            if (isStarted)
            {
                time =  Time.time - startTime;
                timerText.text = ConvertTime((int)time);
            }
        }

        // Đổi số giây thành định dạng phút'giây''
        public string ConvertTime(int time)
        {
            int min = 0, sec = 0;
            string minString, secString;

            min = time / 60;
            sec = time % 60;

            if (sec < 10)
            {
                secString = "0" + sec + "''";
            }
            else
            {
                secString = sec + "''";
            }

            if (min < 10)
            {
                minString = "0" + min + "'";
            }
            else
            {
                minString = min + "'";
            }

            return minString + secString;
        }

        public void UpdateWrongCount(bool isCorrect)
        {
            // +1 cho wrongAttempt nếu trả lời sai
            if (!isCorrect)
            {
                wrongAttemps++;
            }
        }

        public void Submit()
        {
            foreach (GameObject dropZone in dropZones)
            {
                DropZone dropZoneScript = dropZone.GetComponent<DropZone>();

                if (dropZoneScript.isCorrect)
                {
                    correctAnswers++;
                }
                else
                {
                    wrongAnswers++;
                }
            }

            isStarted = false;
            Debug.Log("Correct answers: " + correctAnswers + ", Wrong answers: " + wrongAnswers + ", Wrong attempts: " + wrongAttemps + ", Time: " + ConvertTime((int)time));
            GameSceneManager.instance.FinishGameStep(new GameResult(correctAnswers, wrongAnswers, wrongAttemps, (int)time));
        }

        public void StartTimer()
        {
            // Đánh dấu thời gian bắt đầu
            startTime = Time.time;
            isStarted = true;
        }
    }
}
