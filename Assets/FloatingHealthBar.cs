using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cameraForSlider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public Image fillImage;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //changes the position and rotation to make the floating health bar static
        transform.rotation = cameraForSlider.transform.rotation;
        transform.position = target.position + offset;
    }
}
