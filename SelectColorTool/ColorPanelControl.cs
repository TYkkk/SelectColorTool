using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanelControl : MonoBehaviour
{
    public BoxSlider boxSlider;
    public Slider hueSlider;
    public Image TargetImg;

    public InputField RInputField;
    public InputField GInputField;
    public InputField BInputField;
    public InputField AInputField;

    public Slider RSlider;
    public Slider GSlider;
    public Slider BSlider;
    public Slider ASlider;

    private Color currentColor;
    private Color CurrentColor
    {
        get
        {
            return currentColor;
        }
        set
        {
            currentColor = value;
            TargetImg.color = currentColor;
        }
    }

    private float h;
    private float H
    {
        get
        {
            return h;
        }
        set
        {
            h = value;
            UpdateHSVColor();
        }
    }

    private float s;
    private float S
    {
        get
        {
            return s;
        }
        set
        {
            s = value;
            UpdateHSVColor();
        }
    }

    private float v;
    private float V
    {
        get
        {
            return v;
        }
        set
        {
            v = value;
            UpdateHSVColor();
        }
    }

    private float r = 1f;
    private float R
    {
        get
        {
            return r;
        }
        set
        {
            r = value;
            UpdateColor();
        }
    }

    private float g = 1f;
    private float G
    {
        get
        {
            return g;
        }
        set
        {
            g = value;
            UpdateColor();
        }
    }

    private float b = 1f;
    private float B
    {
        get
        {
            return b;
        }
        set
        {
            b = value;
            UpdateColor();
        }
    }

    private float a = 1f;
    private float A
    {
        get
        {
            return a;
        }
        set
        {
            a = value;
            UpdateColor();
        }
    }

    private void UpdateHSVColor()
    {
        Color color = HSVUtil.ConvertHsvToRgb(h * 360, s, v, a);
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
        CurrentColor = color;
        UpdateUIData();
    }

    private void UpdateColor()
    {
        HsvColor hsv = HSVUtil.ConvertRgbToHsv(r, g, b);
        boxSlider.normalizedValue = hsv.normalizedS;
        boxSlider.normalizedValueY = hsv.normalizedV;
        hueSlider.normalizedValue = hsv.normalizedH;
        Color color = new Color(r, g, b, a);
        CurrentColor = color;
        UpdateUIData();
    }

    private void UpdateUIData()
    {
        RInputField.text = R.ToString();
        GInputField.text = G.ToString();
        BInputField.text = B.ToString();
        AInputField.text = A.ToString();
        RSlider.value = R;
        GSlider.value = G;
        BSlider.value = B;
        ASlider.value = A;
    }

    private void Awake()
    {
        boxSlider.onValueChanged.AddListener(BoxSliderValueChangedEvent);
        hueSlider.onValueChanged.AddListener(HueSliderValueChangedEvent);
        RInputField.onEndEdit.AddListener(RInputFieldEndEditEvent);
        GInputField.onEndEdit.AddListener(GInputFieldEndEditEvent);
        BInputField.onEndEdit.AddListener(BInputFieldEndEditEvent);
        AInputField.onEndEdit.AddListener(AInputFieldEndEditEvent);
        RSlider.onValueChanged.AddListener(RSliderValueChangedEvent);
        GSlider.onValueChanged.AddListener(GSliderValueChangedEvent);
        BSlider.onValueChanged.AddListener(BSliderValueChangedEvent);
        ASlider.onValueChanged.AddListener(ASliderValueChangedEvent);
    }

    private void OnDestroy()
    {
        boxSlider.onValueChanged.RemoveListener(BoxSliderValueChangedEvent);
        hueSlider.onValueChanged.RemoveListener(HueSliderValueChangedEvent);
        RInputField.onEndEdit.RemoveListener(RInputFieldEndEditEvent);
        GInputField.onEndEdit.RemoveListener(GInputFieldEndEditEvent);
        BInputField.onEndEdit.RemoveListener(BInputFieldEndEditEvent);
        AInputField.onEndEdit.RemoveListener(AInputFieldEndEditEvent);
        RSlider.onValueChanged.RemoveListener(RSliderValueChangedEvent);
        GSlider.onValueChanged.RemoveListener(GSliderValueChangedEvent);
        BSlider.onValueChanged.RemoveListener(BSliderValueChangedEvent);
        ASlider.onValueChanged.RemoveListener(ASliderValueChangedEvent);
    }

    public void SetTargetColor(Color color)
    {
        SetTargetColor(color.r, color.g, color.b, color.a);
    }

    public void SetTargetColor(float r, float g, float b, float a = 1f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color GetSelectColor()
    {
        return currentColor;
    }

    private void RSliderValueChangedEvent(float value)
    {
        R = value;
    }

    private void GSliderValueChangedEvent(float value)
    {
        G = value;
    }

    private void BSliderValueChangedEvent(float value)
    {
        B = value;
    }

    private void ASliderValueChangedEvent(float value)
    {
        A = value;
    }

    private void RInputFieldEndEditEvent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        float r = float.Parse(content);
        R = r;
    }

    private void GInputFieldEndEditEvent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        float g = float.Parse(content);
        G = g;
    }

    private void BInputFieldEndEditEvent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        float b = float.Parse(content);
        B = b;
    }

    private void AInputFieldEndEditEvent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        float a = float.Parse(content);
        A = a;
    }

    private void BoxSliderValueChangedEvent(float saturation, float brightness)
    {
        S = saturation;
        V = brightness;
    }

    private void HueSliderValueChangedEvent(float hue)
    {
        H = hue;
    }
}
