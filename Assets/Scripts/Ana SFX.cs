using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFX : MonoBehaviour
{

public AudioSource source;
public AudioClip clip;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(clip, 0.3f);
        }
    }

}
