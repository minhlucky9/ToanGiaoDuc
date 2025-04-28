using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChoosePiece
{
    public class CornerPiece : MonoBehaviour
    {
        public GameObject pieceCorner; 
        public GameObject pieceModel; // Model của miếng ghép
        public GameObject pieceBase; // Miếng to để ghép vào

        public Button btnAnswer;

        Vector3 offset;

        ChoosePieceManager choosePieceManager;

        public bool isCorrectPiece = false;

        // Start is called before the first frame update
        void Start()
        {
            if (pieceCorner == null && transform.childCount > 0)
                pieceCorner = transform.GetChild(0).gameObject;

            if (pieceModel == null && pieceCorner.transform.childCount > 0)
                pieceModel = pieceCorner.transform.GetChild(0).gameObject;

            btnAnswer.onClick.AddListener(CheckCorrectness);

            offset = pieceCorner.transform.InverseTransformPoint(pieceModel.transform.position);
            pieceCorner.transform.localPosition = -offset;

            pieceBase = GameObject.FindGameObjectWithTag("BasePiece");
            choosePieceManager = GameObject.FindGameObjectWithTag("ChoosePieceManager").GetComponent<ChoosePieceManager>();
        }

        public void CheckCorrectness()
        {
            if (isCorrectPiece)
            {
                MoveToBase();
                choosePieceManager.correctAnswers++;
                choosePieceManager.DisableButtons();
            }
            else
            {
                PlayWrongFeedback();
                choosePieceManager.wrongAttemps++;
            }
        }

        public void MoveToBase()
        {
            if (pieceCorner == null)
            {
                Debug.LogWarning("No pieceModel assigned!");
                return;
            }

            // Đổi parent
            pieceCorner.transform.SetParent(pieceBase.transform);

            // Reset local rotation để khớp theo pieceBase, dịch pieceCorner về đúng vị trí (0, 0, 0)
            StartCoroutine(MoveToTarget());

            // Optional: disable inspector sau khi gắn
            /*if (TryGetComponent<PieceInspector>(out var inspector))
            {
                inspector.enabled = false;
            }*/
        }

        public void PlayWrongFeedback()
        {
            StartCoroutine(Shake());
        }

        IEnumerator MoveToTarget()
        {
            Vector3 startPos = pieceCorner.transform.localPosition;
            Quaternion startRot = pieceCorner.transform.localRotation;

            float time = 0;
            float duration = .5f;

            while (time < duration)
            {
                pieceCorner.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, time / duration);
                pieceCorner.transform.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            pieceCorner.transform.localPosition = Vector3.zero;
            pieceCorner.transform.localRotation = Quaternion.identity;
        }

        IEnumerator Shake()
        {
            Vector3 originalPos = transform.position;
            float duration = 0.25f;
            float strength = 0.025f;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                transform.position = originalPos + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.position = originalPos;
        }
    }
}
