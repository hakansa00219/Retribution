using System.Collections;
using System.Linq;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    public Animator animator;
    public string animationName;
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
            
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            // Pause game time
            Time.timeScale = 0;

            // Play the animation
            animator.Play(animationName);

            // Start a coroutine to wait for the animation to finish
            StartCoroutine(ResumeTimeAfterAnimation());
        }
    }

    private IEnumerator ResumeTimeAfterAnimation()
    {
        // Wait for the animation to complete
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        float animationLength = controller.animationClips.First(clip => clip.name == animationName).length + 1;

        // Use unscaled time to track the animation duration
        yield return new WaitForSecondsRealtime(animationLength);

        // Resume game time
        Time.timeScale = 1;
        animator.updateMode = AnimatorUpdateMode.Normal;
        isAnimationPlaying = false;
    }
}
