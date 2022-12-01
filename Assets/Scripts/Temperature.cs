using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    public float MaxTemperature = 3f;
    private float temperature = 0f;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color flashColor = Color.black;

    [SerializeField] GameObject overHeatEffect;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip overHeatSound;
    private AudioSource audioSource;


    private LevelLoader sceneManager;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<LevelLoader>();
    }

    public virtual void Heat(float level, Vector2 position)
    {
        temperature += level;
        audioSource.PlayOneShot(hurtSound, 0.3f);
        StartCoroutine(Flash());
        CheckTemperature();
    }

    private IEnumerator Flash()
    {
        foreach (var sprite in bodyParts)
        {
            sprite.color = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (var sprite in bodyParts)
        {
            sprite.color = Color.white;
        }
    }

    private void CheckTemperature()
    {
        if (temperature > MaxTemperature)
        {
            OverHeat();
        }
    }

    public virtual void OverHeat()
    {
        audioSource.PlayOneShot(overHeatSound, 0.1f);
        Instantiate(overHeatEffect, transform.position, Quaternion.identity);
        GetComponent<Inventory>().DropAll();

        //FIXME
        if (gameObject.CompareTag("Player"))
        {
            sceneManager.RestartLevel();
        }

        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
