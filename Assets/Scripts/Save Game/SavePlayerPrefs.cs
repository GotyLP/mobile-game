using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    public Transform jugadorTransform; // Asigna el transform del jugador
    public int vida = 100;
    public int puntos = 0;

    string rutaArchivo => Path.Combine(Application.persistentDataPath, "jugador.json");

    public void GuardarDatos()
    {
        SaveData data = new SaveData
        {
            posX = jugadorTransform.position.x,
            posY = jugadorTransform.position.y,
            posZ = jugadorTransform.position.z,
            life = vida,
            Points = puntos
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Datos guardados en: " + rutaArchivo);
    }

    public void CargarDatos()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            jugadorTransform.position = new Vector3(data.posX, data.posY, data.posZ);
            vida = data.life;
            puntos = data.Points;

            Debug.Log("Datos cargados: Vida = " + vida + ", Puntos = " + puntos);
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado.");
        }
    }
}