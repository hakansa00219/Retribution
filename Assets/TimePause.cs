using System.Collections;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    public Animator animator;
    public string animationTrigger;
    private bool isAnimationPlaying = false;

    void Update()
    {
        if (isAnimationPlaying)
        {
            // If you need specific updates during pause, use Time.unscaledDeltaTime
        }
    }

    public void PlayAnimationAndPauseTime()
    {
        if (animator != null)
        {
            isAnimationPlaying = true;

            // Pause game time
            Time.timeScale = 0;

            // Play the animation
            animator.SetTrigger(animationTrigger);

            // Start a coroutine to wait for the animation to finish
            StartCoroutine(ResumeTimeAfterAnimation());
        }
    }

    private IEnumerator ResumeTimeAfterAnimation()
    {
        // Wait for the animation to complete
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        // Use unscaled time to track the animation duration
        yield return new WaitForSecondsRealtime(animationLength);

        // Resume game time
        Time.timeScale = 1;
        isAnimationPlaying = false;
    }
}
