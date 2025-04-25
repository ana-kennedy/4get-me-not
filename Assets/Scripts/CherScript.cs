using UnityEngine;

public class CherScript : MonoBehaviour

{

    public Animator animator;
    
    void Start()
    {
        
    }

 
    void Update()
    {
        animator.SetBool("Smoking", true);
    }



}
