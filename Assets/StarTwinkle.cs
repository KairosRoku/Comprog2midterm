using UnityEngine;

public class StarTwinkle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float twinkleDuration;
    private float timer = 0f;
    private bool fadingIn;
    private float minAlpha;
    private float maxAlpha;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        minAlpha = Random.Range(0.1f, 0.5f);
        maxAlpha = Random.Range(0.6f, 1f); 

  
        twinkleDuration = Random.Range(1f, 3f);

        timer = Random.Range(0f, twinkleDuration);

        fadingIn = Random.Range(0, 2) == 0;

        SetAlpha(fadingIn ? minAlpha : maxAlpha);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (fadingIn)
        {
            SetAlpha(Mathf.Lerp(minAlpha, maxAlpha, timer / twinkleDuration));
            if (timer >= twinkleDuration)
            {
                timer = 0f;
                fadingIn = false;
            }
        }
        else
        {
            SetAlpha(Mathf.Lerp(maxAlpha, minAlpha, timer / twinkleDuration));
            if (timer >= twinkleDuration)
            {
                timer = 0f;
                fadingIn = true;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(alpha); 
        spriteRenderer.color = color;
    }
}
