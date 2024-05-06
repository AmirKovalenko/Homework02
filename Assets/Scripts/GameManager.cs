using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] private FireHazard[] fireHazard;
    [SerializeField] private WaterHazard[] waterHazard;
    [SerializeField] public NPCController indexCounter;
    private void Start()
    {
        foreach (FireHazard fireHazard in fireHazard)
            fireHazard.onCharacterEnteredAction += HandleCharacterOnFire;
        foreach (WaterHazard waterHazard in waterHazard)
            waterHazard.onCharacterEnteredAction += HandleCharacterOnWater;
    }

    public void HandleCharacterOnFire(FireEnteredEventArgs args)
    {
        args.playerController.TakeDamage(args.damageDealt);
    }

    public void HandleCharacterOnWater(WaterEnteredEventArgs args)
    {
        args.playerController.TakeDamage(args.damageDealt);
    }

}
