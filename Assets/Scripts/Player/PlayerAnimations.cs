using Photon.Pun;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayJumpAnim() => animator.SetTrigger("Jump");
    public void StopJumpAnim() => animator.ResetTrigger("Jump");

    public void PlayRunAnim(bool state) => animator.SetBool("IsMoving", state);

    public void PlayDeathAnim(bool isHealthAvail)
    {
        animator.SetTrigger("Death");
        if (isHealthAvail)
        {
            PlayRecoverAnim();
        }
    }
    
    private void PlayRecoverAnim() => animator.SetBool("IsRecoverable", true);

    public void PlayAttackAnim() => animator.SetTrigger("Attack");

    void ResetDeathAnim() => animator.ResetTrigger("Death");
}
