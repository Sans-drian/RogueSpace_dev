using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Health playerHealth;
    public UnityEngine.UI.Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // update the main player health slider
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;

        if(fillValue <= slider.maxValue / 2)
        {
            fillImage.color = Color.red; //change color of healthbar after 2 hits
        }
        //Debug.Log(fillValue);
        slider.value = fillValue; 
    }
}
