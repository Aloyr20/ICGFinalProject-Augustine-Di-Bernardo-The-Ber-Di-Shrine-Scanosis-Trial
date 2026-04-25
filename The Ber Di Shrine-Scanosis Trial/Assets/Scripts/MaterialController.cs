using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Material targetMaterial;

    [Range(0f, 1f)]
    public float glowAmount = 0f;

    [ColorUsage(true, false)]
    public Color baseColor = Color.gray;

    [ColorUsage(true, true)]
    public Color glowColor = new Color(4.031f, 1.561f, 0f, 1f);

    public float maxGlowIntensity = 3f;

    public float minFresnelPower = 5f;
    public float maxFresnelPower = 1f;

    public float minPulseSpeed = 0f;
    public float maxPulseSpeed = 2f;

    public float minPulseMinStrength = 0.8f;
    public float maxPulseMinStrength = 0.3f;

    public float minPulseMaxStrength = 1f;
    public float maxPulseMaxStrength = 1.5f;

    private Color originalBaseColor;
    private Color originalGlowColor;
    private float originalFresnelPower;
    private float originalPulseSpeed;
    private float originalPulseMin;
    private float originalPulseMax;
    private bool originalsCaptured = false;

    void OnEnable()
    {
        if (targetMaterial == null)
        {
            return;
        }

        if (!originalsCaptured)
        {
            originalBaseColor = targetMaterial.GetColor("_Color");
            originalGlowColor = targetMaterial.GetColor("_GlowColor");
            originalFresnelPower = targetMaterial.GetFloat("_FresnelPower");
            originalPulseSpeed = targetMaterial.GetFloat("_PulseSpeed");
            originalPulseMin = targetMaterial.GetFloat("_PulseMinStrength");
            originalPulseMax = targetMaterial.GetFloat("_PulseMaxStrength");
            originalsCaptured = true;
        }
    }

    void Update()
    {
        if (targetMaterial == null)
        {
            return;
        }

        Color blendedBase = Color.Lerp(baseColor, Color.white, glowAmount * 0.2f);
        targetMaterial.SetColor("_Color", blendedBase);

        Color glow = glowColor * Mathf.Pow(2f, maxGlowIntensity * glowAmount);
        targetMaterial.SetColor("_GlowColor", glow);

        float fresnel = Mathf.Lerp(minFresnelPower, maxFresnelPower, glowAmount);
        targetMaterial.SetFloat("_FresnelPower", fresnel);

        float pulseSpeed = Mathf.Lerp(minPulseSpeed, maxPulseSpeed, glowAmount);
        targetMaterial.SetFloat("_PulseSpeed", pulseSpeed);

        float pulseMin = Mathf.Lerp(minPulseMinStrength, maxPulseMinStrength, glowAmount);
        targetMaterial.SetFloat("_PulseMinStrength", pulseMin);

        float pulseMax = Mathf.Lerp(minPulseMaxStrength, maxPulseMaxStrength, glowAmount);
        targetMaterial.SetFloat("_PulseMaxStrength", pulseMax);
    }

    void OnDisable()
    {
        if (targetMaterial == null || !originalsCaptured)
        {
            return;
        }

        targetMaterial.SetColor("_Color", originalBaseColor);
        targetMaterial.SetColor("_GlowColor", originalGlowColor);
        targetMaterial.SetFloat("_FresnelPower", originalFresnelPower);
        targetMaterial.SetFloat("_PulseSpeed", originalPulseSpeed);
        targetMaterial.SetFloat("_PulseMinStrength", originalPulseMin);
        targetMaterial.SetFloat("_PulseMaxStrength", originalPulseMax);
    }
}