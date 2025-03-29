using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MathCounting {
    public abstract class CharacterAbstact : NewMonobehavior {
        [SerializeField] protected Animator animator;

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadAnimator();
        }

        protected virtual void LoadAnimator() {
            if (this.animator != null) return;
            this.animator = transform.GetComponentInChildren<Animator>();
            Debug.Log(transform.name + ": LoadAnimator", gameObject);

        }

        public virtual void ChangeAnimation( string animationName, float time = 0.1f ) {
            this.animator.CrossFade(animationName, time);
        }



    }

}