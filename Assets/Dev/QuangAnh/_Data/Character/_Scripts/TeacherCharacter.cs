using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class TeacherCharacter : CharacterAbstact {

        public override void ChangeAnimation( string animationName, float time = 0.1F ) {
            base.ChangeAnimation(animationName, time);
        }
        public virtual void Animation( string animationName, float time = 0.1F ) {
            ChangeAnimation(animationName, time);
        }

    }



}