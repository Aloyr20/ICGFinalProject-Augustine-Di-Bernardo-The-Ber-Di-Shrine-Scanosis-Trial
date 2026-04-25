using UnityEngine;
public class RuneDissolveController : MonoBehaviour
{
    public Material targetMaterial;

    [Range(0f, 1f)]
    public float dissolveAmount = 0f;

    private float originalDissolve;
    private bool captured = false;

    void OnEnable()
    {
        if (targetMaterial == null)
        {
            return;
        }

        if (!captured)
        {
            originalDissolve = targetMaterial.GetFloat("_DissolveAmount");
            captured = true;
        }
    }

    void Update()
    {
        if (targetMaterial == null)
        {
            return;
        }

        targetMaterial.SetFloat("_DissolveAmount", dissolveAmount);
    }

    void OnDisable()
    {
        if (targetMaterial == null || !captured)
        {
            return;
        }

        targetMaterial.SetFloat("_DissolveAmount", originalDissolve);
    }
}
