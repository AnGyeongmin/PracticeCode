using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Header("Q")]
    public Image abilityImage1;
    public float coolTime1 = 5;
    public bool isCooldown1 = false;
    public KeyCode ability1;
    [Space]
    [Header("W")]
    public Image abilityImage2;
    public float coolTime2 = 5;
    public bool isCooldown2 = false;
    public KeyCode ability2;
    [Space]
    [Header("E")]
    public Image abilityImage3;
    public float coolTime3 = 5;
    public bool isCooldown3 = false;
    public KeyCode ability3;
    [Space]
    [Header("R")]
    public Image abilityImage4;
    public float coolTime4 = 5;
    public bool isCooldown4 = false;
    public KeyCode ability4;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        Ability4();
    }

    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown1 == false)
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }
        if (isCooldown1) 
        { 
            abilityImage1.fillAmount -= 1 / coolTime1 * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }
    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / coolTime2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / coolTime3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    void Ability4()
    {
        if (Input.GetKey(ability4) && isCooldown4 == false)
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 4;
        }
        if (isCooldown4)
        {
            abilityImage4.fillAmount -= 1 / coolTime4 * Time.deltaTime;

            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
}
