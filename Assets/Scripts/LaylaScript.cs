using UnityEngine;

public class LaylaScript : MonoBehaviour
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
