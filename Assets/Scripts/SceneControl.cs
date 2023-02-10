using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;

    private void Awake()
    {
        instance = this;
    }
    public void MoveToScene()
    {
        SceneManager.LoadScene("BlastScene");
    }
}
