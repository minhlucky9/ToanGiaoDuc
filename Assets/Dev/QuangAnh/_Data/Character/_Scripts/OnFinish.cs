using UnityEngine;

public class OnFinish : StateMachineBehaviour {
    public bool hasLooped = false; 

    override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
       
        if (stateInfo.normalizedTime >= 1.0f && !hasLooped) {
            hasLooped = true; 
            animator.Play("idle"); 
        }
    }

    override public void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        hasLooped = false; 
    }
}