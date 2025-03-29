using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MathConnection
{
    public class NumberSlot : MonoBehaviour
    {
        public int numberSlot;
        public TextMeshPro textMeshPro;
        public List<DraggableObject> connectedObjects = new();

        private void Awake()
        {
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            if (numberSlot <= 0)
            {
                Debug.LogError("numberSlot phải lớn hơn 0.");
            }
            UpdateText();
        }

        private void UpdateText()
        {
            if (textMeshPro != null)
            {
                textMeshPro.text = numberSlot.ToString();
            }
        }

        public void AddConnection(DraggableObject obj)
        {
            if (obj == null) return;

            if (obj.currentConnectedSlot != null && obj.currentConnectedSlot != this)
            {
                obj.currentConnectedSlot.connectedObjects.Remove(obj);
            }

            if (!connectedObjects.Contains(obj))
            {
                connectedObjects.Add(obj);
                obj.ConnectToSlot(this);
                obj.currentConnectedSlot = this;
                Debug.Log($"Slot {numberSlot} đã kết nối object có objectCount = {obj.objectCount}");

                if (obj.objectCount != numberSlot)
                {
                    GameManager.Instance.AddMistake();
                    Debug.Log("Nối sai -> mistake++");
                }
            }
        }
    }
}