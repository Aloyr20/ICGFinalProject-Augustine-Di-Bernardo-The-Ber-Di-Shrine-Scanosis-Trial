using UnityEngine;

public class BOTWScanner : MonoBehaviour
{
    public float scanSpeed = 5f;
    public float scanRangeMax = 80f;
    public bool allowKeyTrigger = true;

    private float currentRange;
    private bool isScanning;

    void Awake()
    {
        Shader.SetGlobalFloat("_ScanRange", 0f);
        Shader.SetGlobalVector("_ScanStartPos", Vector3.zero);
    }

    void Update()
    {
        if (allowKeyTrigger && Input.GetKeyDown(KeyCode.C))
        {
            TriggerScan();
        }

        if (!isScanning)
        {
            return;
        }

        currentRange += scanSpeed * Time.deltaTime;
        Shader.SetGlobalFloat("_ScanRange", currentRange);

        if (currentRange >= scanRangeMax)
        {
            isScanning = false;
            currentRange = 0f;
            Shader.SetGlobalFloat("_ScanRange", currentRange);
        }
    }

    public void TriggerScan()
    {
        currentRange = 0f;
        isScanning = true;
        Shader.SetGlobalVector("_ScanStartPos", transform.position);
        Shader.SetGlobalFloat("_ScanRange", 0f);
    }
}