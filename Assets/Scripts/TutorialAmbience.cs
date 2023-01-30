using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TutorialAmbience : MonoBehaviour
{
    private ChromaticAberration aberration;
    private LensDistortion distortion;
    
    private void Awake() => GetComponent<Volume>().profile.TryGet(out aberration);
    private void Start() => GetComponent<Volume>().profile.TryGet(out distortion);

    private void Update() => aberration.intensity.value = 0.25f + (Mathf.PerlinNoise(Time.time * 6, Time.time * 6) / 4.0f) * 3.0f;

    public void RemoveLensDistortion()
    {
        distortion.active = false;
        aberration.active = false;
    }

    public void AddLensDistortion()
    {
        distortion.active = true;
        aberration.active = true;
    } 
}
