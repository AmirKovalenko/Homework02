using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public TextMeshProUGUI hpText;

    private void Start()
    {
        currentHP = maxHP;

    }

    private void Update()
    {
        
    }

}
