using UnityEngine;

public class Target : MonoBehaviour
{
    public VictoryByDestruction manager;

    void OnDestroy()
    {
        Debug.Log("objetivo destruidooooooooooooo " );
        if (manager != null)
        {
            manager.RegistrarDestruccion();
        }
    }
}
