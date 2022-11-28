using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutText : MonoBehaviour
{
    private TMPro.TMP_Text text;
    [SerializeField] float fadeSpeed = 2f;
    [SerializeField] float fadeDelay = 1f;
    [SerializeField] float moveSpeed = 20f;
    private float duration = 0f;

    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        transform.Translate(Vector2.up * moveSpeed * delta);
        duration += delta;
        if (duration > fadeDelay)
        {
            text.alpha -= fadeSpeed * delta;
            if (text.alpha < 0.1)
            {
                Destroy(gameObject);
            }
        }
    }
}
