using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LinkPair
{
    public class LinkPairManager : MonoBehaviour
    {
        private Button btnComplete;

        public int correctAnswers = 0;
        public int wrongAnswers;
        public int wrongAttemps = 0;
        private float startTime = 0;
        private float time = 0;
        private bool isStarted = false;

        public int totalQuestions;
        public int[] typeAmount;
        public LinkableObject validLinkable1, validLinkable2;
        public int validType1, validType2;

        // Start is called before the first frame update
        void Start()
        {
            // Lấy btn hoàn thành
            btnComplete = GameObject.FindGameObjectWithTag("BtnComplete").GetComponent<Button>();
            btnComplete.onClick.AddListener(Submit);

            // Lấy các object đề bài đang xét
            validType1 = validLinkable1.type;
            validType2 = validLinkable2.type;

            // Chuẩn bị để tìm totalQuestion = giá trị min trong typeAmount
            totalQuestions = 999;

            // Lấy số lượng các linkable objects theo loại (các loại là các số từ 1 -> x)
            for (int type = 0; type < typeAmount.Length; type++)
            {
                int amount = 0;
                GameObject[] linkableObjects = GameObject.FindGameObjectsWithTag("Linkable");
                foreach (GameObject linkable in linkableObjects)
                {
                    LinkableObject linkableScript = linkable.GetComponent<LinkableObject>();
                    if (linkableScript.type == type + 1)
                    {
                        amount++;
                    }
                }
                typeAmount[type] = amount;

                // Chỉ cập nhật totalQuestion khi loại này đang được xét
                if (amount < totalQuestions && (type + 1 == validType1 || type + 1 == validType2))
                {
                    totalQuestions = amount;
                }
            }

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
            

            isStarted = false;
            Debug.Log("Correct answers: " + correctAnswers + ", Wrong answers: " + wrongAnswers + ", Wrong attempts: " + wrongAttemps + ", Time: " + ConvertTime((int)time));
        }

        public void StartTimer()
        {
            // Đánh dấu thời gian bắt đầu
            startTime = Time.time;
            isStarted = true;
        }
    }
}