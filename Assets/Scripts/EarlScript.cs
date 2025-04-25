using UnityEngine;

public class EarlScript : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        
    }

 
    void Update()
    {
        animator.SetBool("isBlinking", true);
    }

}
