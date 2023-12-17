using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heath : MonoBehaviour
{
    public Slider PlayerSlider3D;
    Slider PlayerSlider2D;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSlider2D = GetComponent<Slider>();
        PlayerSlider2D.maxValue = health;
        PlayerSlider3D.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSlider2D.value = health;
        PlayerSlider3D.value = PlayerSlider2D.value;
    }
}
