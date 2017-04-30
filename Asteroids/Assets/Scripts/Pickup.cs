using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioClip audioClip;    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);

            Destroy(gameObject);
        }
    }
}
