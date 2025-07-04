using UnityEngine;

public class Target : MonoBehaviour
{
    public VictoryByDestruction manager;

    void OnDestroy()
    {
        if (manager != null)
        {
            manager.RegistrarDestruccion();
        }
    }
}
