
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;
using System.Linq;
using Unity.VisualScripting;

namespace MathCounting {

    public class ComparePrefab : InputPrefab {


        [Header("Number Compare Prefab")]
        [SerializeField] protected TextMeshProUGUI numberRightUI;
        [SerializeField] protected TextMeshProUGUI numberLeftUI;

        public TextMeshProUGUI NumberRight => numberRightUI;

        public TextMeshProUGUI NumberLeft => numberLeftUI;

        [Header("Initalize Caculus Prefab")]
        [SerializeField] protected string numberLeft = "";
        [SerializeField] protected string numberRight = "";
        [SerializeField] protected string CorrectAnswer = "";

        protected override void OnValidate() {
            base.OnValidate();
            this.SetChangeValue();
        }

        protected void SetChangeValue() {
           

           this.numberLeftUI.text = numberLeft;
           this.numberRightUI.text = numberRight;
           this.CheckingNumber.SetObj(CorrectAnswer);


        }


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadTextMeshProRight();
            this.LoadTextMeshProLeft();
        }

        protected virtual void LoadTextMeshProRight() {
            if (numberRightUI != null) return;
            this.numberRightUI = this.gameObject.transform.Find("NumberRight").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshProRight " + gameObject);
        }

        protected virtual void LoadTextMeshProLeft() {
            if (numberLeftUI != null) return;
            this.numberLeftUI = this.gameObject.transform.Find("NumberLeft").GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(transform.name + ": LoadTextMeshProLeft " + gameObject);
        }

        

        protected override void OnClick() {
            /*if (MathCtrl.Instance.isAnyActive) return;*/

            MathCtrl.Instance.SetActivePrefab(this);
            if (MathCtrl.Instance.InputTable is Type3 type3) {
               
                Camera mainCamera = Camera.main;
                if (mainCamera == null) {
                    type3.Show();
                    return;
                }
                Vector3 screenPos = mainCamera.WorldToScreenPoint(this.transform.position);
                type3.Show(screenPos);

            } else MathCtrl.Instance.InputTable.Show();

        }

        public virtual void SetNumberLeft(string number) => this.numberLeftUI.text = number;
        public virtual void SetNumberRight(string number) => this.numberRightUI.text = number;
    }
}

