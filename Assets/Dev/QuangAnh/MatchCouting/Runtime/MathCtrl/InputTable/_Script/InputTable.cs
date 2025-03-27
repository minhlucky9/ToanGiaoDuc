using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {
    public abstract class InputTable : NewMonobehavior {

        [SerializeField] protected string inputValue;
        [SerializeField] protected List<StudentCharacter> characters = new();
        public string InputValue => inputValue;


        protected override void Start() {
            this.transform.gameObject.SetActive(false);
        }

        protected override void OnValidate() {
            base.OnValidate();
            this.LoadComponents();
            this.LoadStudents();
        }

        protected virtual void LoadStudents() {
            if (this.characters.Count > 0 && !this.characters.Any(c => c == null)) return;
            this.characters = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
            Debug.Log(transform.name + ": LoadStudents " + gameObject);
        }

        public virtual void ConfirmNumber() {

            string input = MathCtrl.Instance.InputTable.InputValue;

            if (input == null || input == "") input = "0";


            bool isCorrect = MathCtrl.Instance.ActivePrefab.CheckingNumber.DoChecking(input);

            if (isCorrect)
                foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.RIGHT);
            else foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.WRONG);


            MathCtrl.Instance.ActivePrefab.EnterNumber(input);
            MathCtrl.Instance.SetActivePrefab(null);
            MathCtrl.Instance.isAnyActive = false;
            MathCtrl.Instance.InputTable.ClearInputValue();


        }

        public virtual void Show() {
            transform.gameObject.SetActive(true);
        }
        public virtual void Hide() {
            transform.gameObject.SetActive(false);
        }

        public string SetInputValue(string value) {
            inputValue = value;
            return inputValue;
        }

        public virtual string AddInputValue( string value ) {
            
/*            Debug.Log(transform.name + ": AddInputValue " + gameObject);*/

            if(inputValue.Length >= 2) return inputValue;

            inputValue += value;
            return inputValue;
        }
        
        public string ClearInputValue() {
            inputValue = "";
            return inputValue;
        }


    }

}

