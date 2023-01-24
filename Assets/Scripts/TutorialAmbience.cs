using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TutorialAmbience : MonoBehaviour
{
    private ChromaticAberration aberration;
    
    private void Awake() => GetComponent<Volume>().profile.TryGet(out aberration);

    private void Update() => aberration.intensity.value = 0.25f + (Mathf.PerlinNoise(Time.time * 6, Time.time * 6) / 4.0f) * 3.0f;
}
