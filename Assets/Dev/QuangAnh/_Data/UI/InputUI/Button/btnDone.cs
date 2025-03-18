using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MathCounting { 
    public class btnDone : BtnAbstact {

        [SerializeField] protected List<StudentCharacter> characters;

        protected override void OnValidate() {
            base.OnValidate();
            this.LoadComponents();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadStudents();
        }

        protected virtual void LoadStudents() {
            if (this.characters.Count>0 &&!this.characters.Any(c => c == null)) return;
            this.characters = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
            Debug.Log(transform.name + ": LoadStudents " + gameObject);
        }

        protected override void OnClick() {
            MathCounting.Instance.ActiveImagePrefab.ZoomOut();
            MathCounting.Instance.InputTable.Hide();
           this.ConfirmNumber();
            MathCounting.Instance.InputTable.InputField.ClearNumber();
        }

        protected virtual void ConfirmNumber() {

            string input = MathCounting.Instance.InputTable.InputField.TextInputField.text;

            if (input == null) input = "0";

            
            bool isCorrect = MathCounting.Instance.ActiveImagePrefab.CheckingNumber.DoChecking(input);

            if (isCorrect)
                foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.RIGHT);
            else foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.WRONG);


            MathCounting.Instance.ActiveImagePrefab.EnterNumber(input);
            MathCounting.Instance.SetActivePrefab(null);
            MathCounting.Instance.isAnyActive = false;

        }



    }
}
