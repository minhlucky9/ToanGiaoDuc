using MathCounting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.QuangAnh.WallDragDrop {
    public class DragObjectCtrl : NewMonobehavior {
        public List<SlotDrop> slotDrops = new List<SlotDrop>();
        public List<StudentCharacter> studentCharacterList = new List<StudentCharacter>();

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadSlotDrops();
            this.LoadStudentCharacterList();
        }

        protected virtual void LoadSlotDrops() { 
            this.slotDrops = new List<SlotDrop>(FindObjectsOfType<SlotDrop>());
            Debug.Log(transform.name + ": LoadSlotDrops: ", gameObject);
        }

        protected virtual void LoadStudentCharacterList() { 
            if(studentCharacterList.Count > 0 && !studentCharacterList.Any(c => c == null)) return;
            this.studentCharacterList = new List<StudentCharacter>(FindObjectsOfType<StudentCharacter>());
            Debug.Log(transform.name + ": LoadStudentCharacterList: ", gameObject);
        }


        public IEnumerator Animation( bool isCorrect ) {
            string animationType = isCorrect ? ConstAnimator.RIGHT : ConstAnimator.WRONG;


            foreach (StudentCharacter character in this.studentCharacterList) {
                character.ChangeAnimation(animationType);
            }

            yield return new WaitForSeconds(1.2f);

            foreach (StudentCharacter character in this.studentCharacterList) {
                character.ChangeAnimation(ConstAnimator.IDLE);
            }


            yield return new WaitForSeconds(0.2f);
        }
    }
}
