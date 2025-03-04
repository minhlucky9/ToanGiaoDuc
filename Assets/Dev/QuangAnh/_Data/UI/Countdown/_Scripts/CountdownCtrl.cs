using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
namespace MathCounting {

    public class CountdownCtrl : NewMonobehavior {


        [SerializeField] protected PlayableDirector readyTimeLine;
        [SerializeField] protected TeacherCharacter teacherCharacter;
        private bool isGameStarted = false;


        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadPlayableDirector();
            this.LoadTeacherCharacter();
            
        }


        protected virtual void LoadPlayableDirector() {
            if (readyTimeLine != null) return;
            this.readyTimeLine = GetComponentInChildren<PlayableDirector>();
            Debug.Log(transform.name + ": Load PlayableDirector", gameObject);
        }

        protected virtual void LoadTeacherCharacter() { 
            if (teacherCharacter != null) return;
            this.teacherCharacter = FindAnyObjectByType<TeacherCharacter>();
            Debug.Log(transform.name + ": Load TeacherCharacter", gameObject);
        }

        void Update() {
            if (Input.GetMouseButtonDown(0) && !isGameStarted && !EventSystem.current.IsPointerOverGameObject()) {
                StartGame();
            }
        }

        void StartGame() {
            Debug.Log("Start Game");
            isGameStarted = true;
            readyTimeLine.Play();
        }

        public void GuideTalk() {
            teacherCharacter.ChangeAnimation(ConstAnimator.GUIDE_TALK);
        }

        public void EnableInput() {
            
        }
            
    }
    

    

}