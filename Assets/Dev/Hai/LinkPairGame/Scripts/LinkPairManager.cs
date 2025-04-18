using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LinkPair
{
    public class LinkPairManager : MonoBehaviour
    {
        // Enum để chỉnh sửa bộ câu hỏi mỗi pha
        [System.Flags]
        public enum Comparators
        {
            None = 0,
            Greater = 1 << 0,  // >
            Less = 1 << 1,     // <
            Equal = 1 << 2     // =
        }

        private Button btnComplete;

        // Kết quả
        public int correctAnswers = 0;
        public int wrongAnswers;
        public int wrongAttemps = 0;
        private float startTime = 0;
        private float time = 0;
        private bool isStarted = false;

        // Thông tin pha hiện tại
        public int totalQuestions = 0;
        public int validType1, validType2;
        [SerializeField] private List<GameObject> validLinkables1 = new List<GameObject>();
        [SerializeField] private List<GameObject> validLinkables2 = new List<GameObject>();

        // Các object xét cho mỗi pha
        public int currentPhase = 0;
        public RowData[] phases;

        // Đếm số lượng cho pha câu hỏi
        public Dictionary<int, int> typeAmountsDictionary = new Dictionary<int, int>();

        // Tên các nhóm đối tượng
        public string[] typeNames;

        // Chỉ bấm chữa bài 1 lần
        [SerializeField] private bool hasShownAnswer = false;

        private void Awake()
        {
            UpdateObjects();

            // Cập nhật totalQuestion
            int countType1 = typeAmountsDictionary.ContainsKey(validType1) ? typeAmountsDictionary[validType1] : 0;
            int countType2 = typeAmountsDictionary.ContainsKey(validType2) ? typeAmountsDictionary[validType2] : 0;

            totalQuestions = Mathf.Min(countType1, countType2);
        }

        // Start is called before the first frame update
        void Start()
        {
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

        public void UpdateWrongCount(bool isCorrect)
        {
            // +1 cho wrongAttempt nếu trả lời sai
            if (!isCorrect)
            {
                wrongAttemps++;
            }
        }

        public void UpdateAnswerAmount()
        {
            // Đọc qua object trong loại đang xét ít hơn
            if (validLinkables1.Count < validLinkables2.Count)
            {
                foreach (GameObject linkableObject in validLinkables1)
                {
                    LinkableObject linkable = linkableObject.GetComponent<LinkableObject>();
                    // Nếu object đang link với object trong loại đang xét còn lại, tính là đúng, không là sai
                    if (linkable.linkedObject != null && linkable.linkedObject.type == validType2)
                    {
                        correctAnswers++;
                    }
                    else
                    {
                        wrongAnswers++;
                    }
                }
            }
            else
            {
                foreach (GameObject linkableObject in validLinkables2)
                {
                    LinkableObject linkable = linkableObject.GetComponent<LinkableObject>();
                    // Nếu object đang link với object trong loại đang xét còn lại, tính là đúng, không là sai
                    if (linkable.linkedObject != null && (linkable.linkedObject.type == validType1))
                    {
                        correctAnswers++;
                    }
                    else
                    {
                        wrongAnswers++;
                    }
                }
            }
        }

        public void Submit()
        {
            UpdateAnswerAmount();

            Debug.Log("Link pair: Correct answers: " + correctAnswers + ", Wrong answers: " + wrongAnswers + ", Wrong attempts: " + wrongAttemps + ", Time: " + ConvertTime((int)time));
        }

        public void StartTimer()
        {
            // Đánh dấu thời gian bắt đầu
            startTime = Time.time;
            isStarted = true;
        }

        public void CorrectWork()
        {
            if (!hasShownAnswer)
            {
                StartCoroutine(ShowAnswer());
                hasShownAnswer = true;
            }
        }

        IEnumerator ShowAnswer()
        {
            LinkableObject linkObject1;
            LinkableObject linkObject2;
            if (validLinkables1.Count < validLinkables2.Count)
            {
                for (int position = 0; position < validLinkables1.Count; position++)
                {
                    linkObject1 = validLinkables1[position].GetComponent<LinkableObject>();
                    linkObject2 = validLinkables2[position].GetComponent<LinkableObject>();
                    linkObject1.Link(linkObject2);
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                for (int position = 0; position < validLinkables2.Count; position++)
                {
                    linkObject1 = validLinkables2[position].GetComponent<LinkableObject>();
                    linkObject2 = validLinkables1[position].GetComponent<LinkableObject>();
                    linkObject1.Link(linkObject2);
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        public void ChangePhase()
        {
            UpdateAnswerAmount();

            if (currentPhase + 1 < phases.Length)
            {
                currentPhase++;
            }

            hasShownAnswer = false;
        }

        public void UpdateObjects()
        {
            validType1 = phases[currentPhase].validTypes[0];
            validType2 = phases[currentPhase].validTypes[1];

            validLinkables1.Clear();
            validLinkables2.Clear();

            typeAmountsDictionary.Clear();

            GameObject[] linkableObjects = GameObject.FindGameObjectsWithTag("Linkable");
            foreach (GameObject linkable in linkableObjects)
            {
                LinkableObject linkableScript = linkable.GetComponent<LinkableObject>();

                // Lấy số lượng các linkable objects theo loại (các loại là các số từ 1 -> x)
                if (typeAmountsDictionary.ContainsKey(linkableScript.type))
                {
                    typeAmountsDictionary[linkableScript.type]++;
                }
                else
                {
                    typeAmountsDictionary[linkableScript.type] = 1;
                }

                // Chỉnh tất cả linkable object thành không tương tác được
                linkableScript.ChangeOpacity(0.25f);
                linkableScript.ChangeInteractability(false);
                if (linkableScript.linkedObject != null)
                {
                    linkableScript.Unlink();
                }

                // Chỉnh tất cả linkable object trong loại đang xét thành tương tác được
                if (linkableScript.type == validType1)
                {
                    linkableScript.ChangeInteractability(true);
                    linkableScript.ChangeOpacity(1);
                    validLinkables1.Add(linkable);
                }
                else if (linkableScript.type == validType2)
                {
                    linkableScript.ChangeInteractability(true);
                    linkableScript.ChangeOpacity(1);
                    validLinkables2.Add(linkable);
                }
            }
        }
    }

    // Struct để hiển thị mảng 2 chiều
    [System.Serializable]
    public class RowData
    {
        public List<int> validTypes; // Các loại đối tượng trong pha
        public LinkPairManager.Comparators allowedComparators; // Các lựa chọn so sánh cho pha này
    }

    public static class EnumUtils
    {
        public static LinkPairManager.Comparators[] GetFlags(this LinkPairManager.Comparators input)
        {
            List<LinkPairManager.Comparators> result = new List<LinkPairManager.Comparators>();
            foreach (LinkPairManager.Comparators comp in System.Enum.GetValues(typeof(LinkPairManager.Comparators)))
            {
                if (comp == LinkPairManager.Comparators.None) continue;
                if (input.HasFlag(comp)) result.Add(comp);
            }
            return result.ToArray();
        }
    }

}