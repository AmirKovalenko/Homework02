using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI hpText;
    [SerializeField] private PlayerController playerHP;

    private void Start()
    {
        hpText.text = playerHP.CurrentHP.ToString();
    }

}
