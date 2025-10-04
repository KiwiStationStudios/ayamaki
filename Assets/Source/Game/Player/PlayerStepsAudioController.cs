using System;
using Ayamaki.Game.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStepsAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] stepsFX;
    [SerializeField] private AudioSource audioSource;
    private PlayerController player;
    void Start()
    {
        if (player != null)
            return;

        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource == null || stepsFX.Length <= 0)
            return;

        if (player.isMoving && !audioSource.isPlaying)
            audioSource.PlayOneShot(stepsFX[Random.Range(0, stepsFX.Length - 1)]);
    }
}
