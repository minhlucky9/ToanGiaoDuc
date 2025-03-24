
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;
using System.Linq;
using Unity.VisualScripting;

namespace MathCounting {

    public class CaculusPrefab : InputPrefab {
        [Header("Initalize Caculus Prefab")]
        [SerializeField] protected bool isClickable = true;
        [SerializeField] protected string UIAnswer = "";
        [SerializeField] protected string CorrectAnswer = "";

        protected override void OnValidate() {
            base.OnValidate();
            this.SetChangeValue();
        }


        protected override void OnClick() {
           
            if(!isClickable ) return;
            /*if (MathCtrl.Instance.isAnyActive) return;*/

            MathCtrl.Instance.SetActivePrefab(this);
            MathCtrl.Instance.InputTable.Show();

        }

        protected void SetChangeValue() {

            this.number.text = UIAnswer;
            this.CheckingNumber.SetObj(CorrectAnswer);

        }


    }
}

