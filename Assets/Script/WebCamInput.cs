using UnityEngine;

public class WebCamInput : MonoBehaviour
{
    [SerializeField] string webCamName;
    [SerializeField] Vector2 webCamResolution = new Vector2(1920, 1080);
    [SerializeField] Texture staticInput;

    // Provide input image Texture.
    public Texture inputImageTexture{
        get{
            if(staticInput != null) return staticInput;
            return inputRT;
        }
    }

    WebCamTexture webCamTexture;
    RenderTexture inputRT;

    void Start()
    {
        if(staticInput == null){
            webCamTexture = new WebCamTexture(webCamName, (int)webCamResolution.x, (int)webCamResolution.y);
            webCamTexture.Play();
        }

        inputRT = new RenderTexture((int)webCamResolution.x, (int)webCamResolution.y, 0);
    }

    void Update()
    {
        if(staticInput != null) return;
        if(!webCamTexture.didUpdateThisFrame) return;
               
        Graphics.Blit(webCamTexture, inputRT);
    }

    void OnDestroy(){
        if (webCamTexture != null) Destroy(webCamTexture);
        if (inputRT != null) Destroy(inputRT);
    }
}
