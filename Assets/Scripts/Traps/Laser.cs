using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject[] lasers;
    [SerializeField] private Collider laserCollider;
    [SerializeField] private Vector2 randomBlinkDelay;
    private float blinkDelay;

    private float blinkTimer;

    private void Start()
    {
        blinkTimer = Random.Range(0.0f, 2.0f);
        blinkDelay = Random.Range(randomBlinkDelay.x, randomBlinkDelay.y);
    }

    private void Update()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer < blinkDelay) return;

        blinkTimer = 0;
        blinkDelay = Random.Range(randomBlinkDelay.x, randomBlinkDelay.y);
        
        for(int i = 0; i < lasers.Length; i++) lasers[i].SetActive(!lasers[i].activeSelf);

        laserCollider.enabled = !laserCollider.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.GameOver();
    }
}