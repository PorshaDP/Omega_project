using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerController player;

    Image healthBarImage;
    public float maxHealth = 100f;
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();
        player.HP = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = player.HP / maxHealth;
    }
}

//sssss