using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    private LevelLoader sceneManager;

    private void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sceneManager.LoadNextLevel();
        }
    }
}
