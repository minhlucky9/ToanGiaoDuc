using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DragDrop
{
    public class DropZone : MonoBehaviour
    {
        [Header("Số lượng đối tượng yêu cầu")]
        public int requiredCount;


        [Space(20)]
        public bool isCorrect = false;
        public TMP_Text text;
        public  Button btnConfirm;
        [SerializeField] private List<GameObject> containedObjects = new List<GameObject>();
        private bool confirmed = false;
        private DragDropManager manager;

        [Header("Nút của dropzone")]
        // Các ảnh của btn
        [Tooltip("Ảnh gốc")]
        [SerializeField] private Sprite originalButton;
        [Tooltip("Ảnh sau khi bấm nộp")]
        [SerializeField] private Sprite submittedButton;
        // Text của btn
        [Tooltip("Text của nút")]
        [SerializeField] private TMP_Text buttonText;

        [Header("Dạng bài Lớn hơn/Bé hơn/Bằng")]
        // Dùng để điều chỉnh dạng bài tập thành lớn hơn, bé hơn, bằng
        [SerializeField] private bool isGreater = false;
        [SerializeField] private bool isLesser = false;
        [SerializeField] private bool isEqual = true;

        private void OnValidate()
        {
            if (isEqual)
            {
                isLesser = isGreater = false;
            }
            if (isLesser)
            {
                isGreater = isEqual = false;
            }
            if (isGreater)
            {
                isLesser = isEqual = false;
            }
        }

        void Start()
        {
            manager = GameObject.FindGameObjectWithTag("DragDropManager").GetComponent<DragDropManager>();

            // Random số đối tượng yêu cầu nếu game manager đã tích isRandom
            if (manager.isRandom)
            {
                RandomRequiredCount();
            }
            text.text = requiredCount.ToString();
            btnConfirm.onClick.AddListener(SubmitButton);
        }

        public void AddObject(GameObject obj)
        {
            if (!containedObjects.Contains(obj))
            {
                containedObjects.Add(obj);
                ResetButton();
            }
        }

        public void RemoveObject(GameObject obj)
        {
            if (containedObjects.Contains(obj))
            {
                containedObjects.Remove(obj);
                ResetButton();
            }
        }

        // Kiểm tra đúng sai theo yêu cầu đề bài
        public void CheckCorrectness()
        {
            if (isEqual)
            {
                isCorrect = containedObjects.Count == requiredCount;
            }
            if (isGreater)
            {
                isCorrect = containedObjects.Count > requiredCount;
            }
            if (isLesser)
            {
                isCorrect = containedObjects.Count < requiredCount;
            }

            manager.UpdateWrongCount(isCorrect);
            // Đổi btn
            confirmed = true;
        }

        void RandomRequiredCount()
        {
            requiredCount = Random.Range(0, 5);
        }

        void SubmitButton()
        {
            CheckCorrectness();
            btnConfirm.GetComponent<Image>().sprite = submittedButton; // Đổi img của btn
            buttonText.text = ""; // Đổi text của btn
            btnConfirm.interactable = false; // Làm btn không bấm được
        }

        void ResetButton()
        {
            if (confirmed)
            {
                confirmed = false;
                btnConfirm.GetComponent<Image>().sprite = originalButton; // Đổi img của btn
                buttonText.text = "Xong"; // Đổi text của btn
                btnConfirm.interactable = true; // Làm btn bấm được
            }
        }
    }
}
