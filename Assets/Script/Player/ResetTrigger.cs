using UnityEngine;

public class ResetTrigger : StateMachineBehaviour
{
    [SerializeField] string resetTriggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(resetTriggerName);
        
    }
}
