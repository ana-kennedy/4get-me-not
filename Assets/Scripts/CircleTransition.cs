using UnityEngine;

public class CircleTransition : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        animator.SetBool("onEnter", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetBool("onEnter", true);
        }
    }
}
