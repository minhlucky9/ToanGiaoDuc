using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChoosePiece
{
    public class ChoosePieceManager : MonoBehaviour
    {
        GameObject[] cornerPieces;
        CornerPiece correctPiece;
        GameObject[] answerButtons;

        private Button btnComplete;

        public int correctAnswers = 0;
        public int wrongAttemps = 0;
        private float startTime = 0;
        private float time = 0;
        private bool isStarted = false;

        // Chỉ chữa bài 1 lần
        private bool hasShownAnswer = false;

        // Start is called before the first frame update
        void Start()
        {
            cornerPieces = GameObject.FindGameObjectsWithTag("CornerPiece");
            answerButtons = GameObject.FindGameObjectsWithTag("BtnAnswer");

            foreach (GameObject piece in cornerPieces)
            {
                CornerPiece pieceScript = piece.GetComponent<CornerPiece>();

                if (pieceScript.isCorrectPiece)
                {
                    correctPiece = pieceScript;
                    break;
                }
            }

            // Lấy btn hoàn thành
            btnComplete = GameObject.FindGameObjectWithTag("BtnComplete").GetComponent<Button>();
            btnComplete.onClick.AddListener(Submit);

            StartTimer();
        }

        // Update is called once per frame
        void Update()
        {
            if (isStarted)
            {
                time = Time.time - startTime;
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

        public void Submit()
        {
            Debug.Log("Correct answers: " + correctAnswers + ", Wrong attempts: " + wrongAttemps + ", Time: " + ConvertTime((int)time));
        }

        public void StartTimer()
        {
            // Đánh dấu thời gian bắt đầu
            startTime = Time.time;
            isStarted = true;
        }

        // Chữa bài
        public void CorrectWork()
        {
            if (!hasShownAnswer)
            {
                correctPiece.MoveToBase();
                hasShownAnswer = true;
            }
        }

        public void DisableButtons()
        {
            foreach (GameObject objectButton in answerButtons)
            {
                Button button = objectButton.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }
}
