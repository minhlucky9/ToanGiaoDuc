using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {

    public enum InputType {
        Type1,
        Type2,
        Type3,
        Type4,
    }

    public class MathCtrl : Singleton<MathCtrl> {

        [SerializeField] protected InputType inputType;
        [SerializeField] protected InputTable inputTable;
        [SerializeField] protected InputPrefab activePrefab;

        public InputTable InputTable => inputTable;
        public InputPrefab ActivePrefab => activePrefab;

        public bool isAnyActive = false;

        
        protected override void LoadComponents() {
            base.LoadComponents();
            if(inputTable == null) this.LoadInputTable();
        }
        protected override void OnValidate() {
            LoadInputTable();
        }

        protected virtual void LoadInputTable() {
           
            switch (inputType) {
                case InputType.Type1:
                    this.inputTable = FindAnyObjectByType<Type1>();
                    break;
                case InputType.Type2:
                    this.inputTable = FindAnyObjectByType<Type2>();
                    break;
                case InputType.Type3:
                    this.inputTable = FindAnyObjectByType<Type3>();
                    break;
                case InputType.Type4:
                    this.inputTable = FindAnyObjectByType<Type4>();
                    break;
                default:
                    this.inputTable = FindAnyObjectByType<InputTable>();
                    break;
            }
            Debug.Log(transform.name + ": LoadInputTable", gameObject);
        }

        public virtual void SetActivePrefab(InputPrefab prefab) {
            this.activePrefab = prefab;
            this.isAnyActive = true;
        }

        public virtual void ConfirmAnswer() {
            if(activePrefab is ImagePrefab imagePrefab) imagePrefab.ZoomOut();
            this.inputTable.Hide();
            this.inputTable.ConfirmNumber();
        }
    }


}

