using UnityEngine;

public class NHotelBookCheck : MonoBehaviour
{

    public GameObject Birdie;
    public GameObject DialogueBox;
    public GameObject Camera;
    public AudioSource audioSource;
    public AudioClip zapFX;

    void Start()
    {
       if(GameManager.Instance.GetEventState("NAnastasiaBook") && !GameManager.Instance.GetEventState("NHole1"))
       {
        GameManager.Instance.SetEventState("NHole1", true);
        Birdie.SetActive(true);
        audioSource.PlayOneShot(zapFX, 0.5f);
        DialogueBox.SetActive(true);

        if(Camera != null)
        {
            CameraFocusObject cameraFocus = Camera.GetComponent<CameraFocusObject>();
            CameraFollowObject cameraFollow = Camera.GetComponent<CameraFollowObject>();

            cameraFollow.enabled = false;
            cameraFocus.enabled = true;
            Camera.GetComponent<Camera>().orthographicSize = 3f;
        }
       } 
    }
}
