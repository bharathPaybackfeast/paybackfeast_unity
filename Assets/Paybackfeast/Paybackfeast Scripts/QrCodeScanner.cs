using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QrCodeScanner : MonoBehaviour
{
    public RawImage rawImage; // The UI element to display the camera feed
    private WebCamTexture webCamTexture;
    private IBarcodeReader barcodeReader;


    [SerializeField] private TMP_Text out_Text;

    void Start()
    {
        // Initialize the webcam texture
        webCamTexture = new WebCamTexture();
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;
        webCamTexture.Play();

        // Initialize the barcode reader
        barcodeReader = new BarcodeReader();
    }

    void Update()
    {
        // Check if the camera is playing
        if (webCamTexture.isPlaying)
        {
            try
            {
                // Create a Color32 array from the webcam texture
                var pixels = webCamTexture.GetPixels32();
                var width = webCamTexture.width;
                var height = webCamTexture.height;

                // Decode the barcode from the image
                var result = barcodeReader.Decode(pixels, width, height);

                // If a barcode is detected, display the result
                if (result != null)
                {
                    Debug.Log("Decoded Text: " + result.Text);
                    out_Text.text = result.Text;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                out_Text.text = ex.Message;
            }
        }
    }
}
