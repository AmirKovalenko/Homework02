using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireHazard : MonoBehaviour
{
    [SerializeField] private uint Damage => damage;
    [SerializeField] private uint damage = 10;

    public UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    [SerializeField] private UnityEvent<FireEnteredEventArgs> onCharacterEntered;

    private void Start()
    {
        onCharacterEntered.AddListener(onCharacterEnteredAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(message: "entered fire hazard!");
            FireEnteredEventArgs fireData = new FireEnteredEventArgs(damage, other.GetComponent<PlayerController>());
            onCharacterEntered.Invoke(fireData);
        }
    }
}

    public struct FireEnteredEventArgs
    {
        public uint damageDealt;
        public PlayerController playerController;

        public FireEnteredEventArgs(uint damage, PlayerController player)
        {
             damageDealt = damage;
             playerController = player;
        }
    }

