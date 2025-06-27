using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    public AudioClip triggerSound;
    private AudioSource audioSource;

    void Start()
    {
        // Intenta obtener un AudioSource existente o añade uno
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (triggerSound != null)
            {
                audioSource.PlayOneShot(triggerSound);
            }
        }
    }
}
