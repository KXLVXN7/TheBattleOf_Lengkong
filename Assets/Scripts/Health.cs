using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHP = 100;
    private float currentHP = 100;

    [SerializeField] private Image HPBar;

    void Awake()
    {
        /*HPBar = GetComponent<Image>();*/
    }
    void Start()
    {
        currentHP = maxHP;
    }

    public void takeDamage()
    {
        currentHP -= 5;
        Debug.Log(HPBar);
        HPBar.fillAmount= currentHP/maxHP;
        Death();
    }
    public void Death()
    {
        if(HPBar.fillAmount == 0)
        {
            Destroy(gameObject);
        }
    }

}
