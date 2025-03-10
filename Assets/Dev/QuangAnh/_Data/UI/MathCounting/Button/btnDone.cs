using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MathCounting { 
    public class btnDone : BtnAbstact { 

        [SerializeField] protected List<StudentCharacter> characters;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadStudents();
        }

        protected virtual void LoadStudents() {
           if (this.characters.Count > 0) return;
           this.characters = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
           Debug.Log(transform.name + ": LoadStudents " + gameObject);
        }

        protected override void OnClick() { 
           mathCounting.ActiveImagePrefab.ZoomOut();
           inputTable.OnDeactive();
           this.ConfirmNumber();
           inputTable.InputField.ClearNumber();
        }

        protected virtual void ConfirmNumber() {

            string input = this.inputTable.InputField.TextInputField.text;

            if (input == null) input = "0";

            
            bool isCorrect = mathCounting.ActiveImagePrefab.CheckingNumber.DoChecking(input);

            if(isCorrect)
                foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.RIGHT);
            else foreach (StudentCharacter character in this.characters) character.ChangeAnimation(ConstAnimator.WRONG);


            mathCounting.ActiveImagePrefab.EnterNumber(input);
            mathCounting.SetImagePrefab(null);
            mathCounting.isAnyActive = false;


        }

    }
}
