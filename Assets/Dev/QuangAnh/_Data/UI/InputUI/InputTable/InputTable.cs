using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



namespace MathCounting {
    public class InputTable : NewMonobehavior {

        [SerializeField] protected string inputValue;
        [SerializeField] protected List<StudentCharacter> characters;
        public string InputValue => inputValue;

        protected override void Awake() {
            base.Awake();
        }

        protected override void Start() {
            this.transform.gameObject.SetActive(false);
        }

        protected override void OnValidate() {
            base.OnValidate();
            this.LoadComponents();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadStudents();
        }

        protected virtual void LoadStudents() {
            if (this.characters.Count > 0 && !this.characters.Any(c => c == null)) return;
            this.characters = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
            Debug.Log(transform.name + ": LoadStudents " + gameObject);
        }

        public virtual void ConfirmNumber() {

            string input = MathCounting.Instance.InputTable.InputValue;

            if (input == null) input = "0";


            bool isCorrect = MathCounting.Instance.ActiveImagePrefab.CheckingNumber.DoChecking(input);

            if (isCorrect)
                foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.RIGHT);
            else foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.WRONG);


            MathCounting.Instance.ActiveImagePrefab.EnterNumber(input);
            MathCounting.Instance.SetActivePrefab(null);
            MathCounting.Instance.isAnyActive = false;
            MathCounting.Instance.InputTable.ClearInputValue();


        }



        public void Show() {
            transform.gameObject.SetActive(true);
        }
        public void Hide() {
            transform.gameObject.SetActive(false);
        }

        public string SetInputValue(string value) {
            inputValue = value;
            return inputValue;
        }

        public virtual string AddInputValue( string value ) {
            inputValue += value;
            return inputValue;
        }
        
        public string ClearInputValue() {
            inputValue = null;
            return inputValue;
        }


    }

}

