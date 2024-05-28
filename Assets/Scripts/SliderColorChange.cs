using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorChange : MonoBehaviour
{
    [SerializeField]
    private float target_H_MAX;
    [SerializeField]
    private float target_H_MIN;

    private GameObject fill_image;
    private Slider this_slider;
    private Color fill_color;
    private float H, S, V;
    // Start is called before the first frame update
    void Start()
    {
        this_slider = GetComponent<Slider>();
        fill_image = this_slider.fillRect.gameObject;
        Color.RGBToHSV(fill_image.GetComponent<Image>().color, out H, out S, out V);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float val = (target_H_MAX - target_H_MIN) * this_slider.value;
        Color targetColor = Color.HSVToRGB(((target_H_MIN+val)/ 360.0f),S,V);
        fill_color = targetColor;
        fill_image.GetComponent<Image>().color = fill_color;
    }
}
