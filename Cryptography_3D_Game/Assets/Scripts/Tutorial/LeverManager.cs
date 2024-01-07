using UnityEngine;

public class LeverManager : MonoBehaviour
{
    private Animator leverAnimator;

    private void Start()
    {
        leverAnimator = GetComponent<Animator>();
    }

    public void PullLever()
    {
        leverAnimator.SetBool("IsPulled", true);
    }
}
