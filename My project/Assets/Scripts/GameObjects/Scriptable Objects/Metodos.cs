using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Metodos", menuName = "Scriptables/Methods")]
public class Metodos : ScriptableObject
{
    public void ReinciarJuego()
    {
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void StartGame(GameObject panel)
    {

        GameManager._Instance.StartCoroutine(GameManager._Instance.SpawnEnemies);
        GameManager._Instance.GM_Player.SetUpDoors();
        panel.SetActive(false);

        foreach(GameObject tarrain in GameManager._Instance.GM_Player.terrains)
        {
            tarrain.layer = 8;
        }
    }
}
