using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterHazard : MonoBehaviour
{
    [SerializeField] private uint Damage => damage;
    [SerializeField] private uint damage = 5;

    [SerializeField] private UnityEvent<WaterEnteredEventArgs> onCharacterEntered;
    public UnityAction<WaterEnteredEventArgs> onCharacterEnteredAction;
    private PlayerController playerController;

    private void Start()
    {
        onCharacterEntered.AddListener(onCharacterEnteredAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerController == null)
                playerController = other.GetComponent<PlayerController>();
            Debug.Log(message:"entered water hazard!");
            playerController.fallAnimation.Play();
            playerController.areControlsLocked = true;
            WaterEnteredEventArgs waterData = new WaterEnteredEventArgs(damage, playerController);
            onCharacterEntered.Invoke(waterData);

            if (playerController.fallAnimation.Play()) 
            {
                Debug.Log(message:"Recover animation");
                playerController.fallRecoverAnimation.Play();
                playerController.areControlsLocked = false;
            }
        }
    }
}
    public struct WaterEnteredEventArgs
    {
        public uint damageDealt;
        public PlayerController playerController;

        public WaterEnteredEventArgs(uint damage, PlayerController player)
        {
            damageDealt = damage;
            playerController = player;
        }
    }

