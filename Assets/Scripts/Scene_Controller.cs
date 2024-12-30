using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan namespace SceneManagement

public class Scene_Controller : MonoBehaviour
{
    public void open_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name); // Ganti Application.LoadLevel dengan SceneManager.LoadScene
    }

    public void exit()
    {
        Application.Quit();
    }
}
