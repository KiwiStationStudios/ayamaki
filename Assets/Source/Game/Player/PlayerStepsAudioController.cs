using System;
using System.Collections.Generic;
using Ayamaki.Core.Shared;
using Ayamaki.Game.Player;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(MaterialScanner))]
public class PlayerStepsAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MaterialSound[] materials;
    private Dictionary<string, MaterialSound> materialDict = new();
    
    private MaterialScanner matscan;
    private PlayerController player;

    [Serializable]
    class MaterialSound
    {
        public string name = "";
        public AudioClip[] audioVariations;

        public void PlayAudioVariation(AudioSource source)
        {
            if (audioVariations.Length > 0 && !source.isPlaying)
                source.PlayOneShot(audioVariations[Random.Range(0, audioVariations.Length - 1)]);
        }
    }
    void Start()
    {
        if (player != null)
            return;

        player = GetComponent<PlayerController>();

        if (matscan != null)
            return;

        matscan = GetComponent<MaterialScanner>();

        // build index //
        materialDict.Clear();

        for (int i = 0; i < materials.Length; i++)
        {
            MaterialSound mat = materials[i];

            materialDict.Add(mat.name, mat);
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (materialDict[matscan.currentMaterialName] == null || matscan.currentMaterialName == "")
            return;

        
        materialDict[matscan.currentMaterialName].PlayAudioVariation(audioSource);
    }
}
