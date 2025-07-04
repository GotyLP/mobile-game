using UnityEngine;
using UnityEngine.UI;

public class VictoryByDestruction : MonoBehaviour
{
    public int objetivosNecesarios = 5;
    private int objetivosDestruidos = 0;

    public Text contadorTexto;        // UI que muestra el progreso
    public GameObject victoryPanel;   // Cartel de victoria (UI Panel)

    void Start()
    {
        ActualizarUI();
        victoryPanel.SetActive(false);
    }

    public void RegistrarDestruccion()
    {
        objetivosDestruidos++;
        ActualizarUI();

        if (objetivosDestruidos >= objetivosNecesarios)
        {
            MostrarVictoria();
        }
    }

    void ActualizarUI()
    {
        contadorTexto.text = "Objetivos: " + objetivosDestruidos + " / " + objetivosNecesarios;
    }

    void MostrarVictoria()
    {
        victoryPanel.SetActive(true);
    }
}
