using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Color glowColor = Color.green;
    public float glowIntensity = 1.0f;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", glowColor * glowIntensity);
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color finalColor = glowColor * Mathf.LinearToGammaSpace(emission * glowIntensity);
        material.SetColor("_EmissionColor", finalColor);
    }
}
