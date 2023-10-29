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

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.35f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void takeDamage()
    {
        currentHP -= 5;
        Debug.Log(HPBar);
        HPBar.fillAmount= currentHP/maxHP;
        StartCoroutine(VisualIndicator(Color.red));
        Death();
    }

    public void Death()
    {
        if(HPBar.fillAmount == 0)
        {
            //GetComponent<Animator>().SetTrigger("PlayerDead");
            Destroy(gameObject);
        }
    }

}
