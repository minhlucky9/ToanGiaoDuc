using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
namespace MathCounting {

    public class CountdownCtrl : Singleton<CountdownCtrl> {


        [SerializeField] protected PlayableDirector readyTimeLine;
        [SerializeField] protected TeacherCharacter teacherCharacter;

        public bool isGameStarted = false;

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

        protected override void Start() {
            StartGame();
        }

        void StartGame() {
            readyTimeLine.Play();
        }

        public void GuideTalk() {
            teacherCharacter.ChangeAnimation(ConstAnimator.GUIDE_TALK);
        }

        public void ToggleStart() {
            this.isGameStarted = true;
        }
            
    }
    

    

}