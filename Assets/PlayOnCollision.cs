using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;

    private AudioSource audioSource;

    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            AudioClip selectedClip = Random.value > 0.5f ? sound1 : sound2;

            if (selectedClip != null)
            {
                audioSource.PlayOneShot(selectedClip);
            }
        }
    }
}
