using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxSlider), typeof(RawImage)), ExecuteInEditMode()]
public class SVBoxSlider : MonoBehaviour
{
    public Slider HueSlider;
    private BoxSlider slider;
    private RawImage image;

    public ComputeShader compute;
    private int kernelID;
    private RenderTexture renderTexture;
    private int textureWidth = 100;
    private int textureHeight = 100;

    public RectTransform rectTransform
    {
        get
        {
            return transform as RectTransform;
        }
    }

    private void Awake()
    {
        slider = GetComponent<BoxSlider>();
        image = GetComponent<RawImage>();
        RectTransform imgRect = image.GetComponent<RectTransform>();
        textureWidth = (int)imgRect.sizeDelta.x;
        textureHeight = (int)imgRect.sizeDelta.y;
        InitializeCompute();
        RegenerateSVTexture();

        HueSlider.onValueChanged.AddListener((float value) =>
        {
            RegenerateSVTexture();
        });
    }

    private void InitializeCompute()
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(textureWidth, textureHeight, 0, RenderTextureFormat.RGB111110Float);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
            renderTexture.name = "rd";
        }

        //compute = Resources.Load<ComputeShader>("Shaders/Compute/GenerateSVTexture");
        kernelID = compute.FindKernel("CSMain");

        image.texture = renderTexture;
    }

    private void OnDestroy()
    {
        if (image.texture != null)
        {
            renderTexture.Release();
            DestroyImmediate(renderTexture);
            image.texture = null;
        }
    }

    private void RegenerateSVTexture()
    {
        compute.SetTexture(kernelID, "Texture", renderTexture);
        compute.SetFloats("TextureSize", textureWidth, textureHeight);
        compute.SetFloat("Hue", HueSlider.value);

        compute.SetBool("linearColorSpace", QualitySettings.activeColorSpace == ColorSpace.Linear);

        var threadGroupsX = Mathf.CeilToInt(textureWidth / 32f);
        var threadGroupsY = Mathf.CeilToInt(textureHeight / 32f);

        compute.Dispatch(kernelID, threadGroupsX, threadGroupsY, 1);
    }
}
