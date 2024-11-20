using UnityEngine;

public class TexturePainter : MonoBehaviour
{
    public Camera paintingCamera;         
    public RenderTexture paintTexture;    
    public Material paintMaterial;        
    public Color brushColor = Color.red;  
    public float brushSize = 0.05f;       
    public float paintPercent = 0f;       
    public LayerMask paintableLayer;      

    private Texture2D tempTexture;        
    private bool[,] paintedPixels;        
    private int totalPaintedPixels = 0;   
    private int totalPixels;             

    public bool canPaint = true;

    private void Start()
    {
        //Initialize the temporary texture to match the render texture size
        tempTexture = new Texture2D(paintTexture.width, paintTexture.height, TextureFormat.RGBA32, false);

        //Initialize paintedPixels array
        paintedPixels = new bool[paintTexture.width, paintTexture.height];

        //Calculate total pixels in the texture
        totalPixels = paintTexture.width * paintTexture.height;
    }

    private void Update()
    {
        if (!canPaint) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = paintingCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, paintableLayer)) // Check for paintable layer
            {
                PaintAt(hit.textureCoord);
                CalculatePaintedPercentage();
            }
        }
    }

    private void PaintAt(Vector2 uv)
    {
        RenderTexture.active = paintTexture;

        //Read RenderTexture into tempTexture
        tempTexture.ReadPixels(new Rect(0, 0, paintTexture.width, paintTexture.height), 0, 0);
        tempTexture.Apply();

        //Convert UV coordinates to pixel coordinates
        int x = Mathf.FloorToInt(uv.x * paintTexture.width);
        int y = Mathf.FloorToInt(uv.y * paintTexture.height);

        //Calculate brush radius
        int radius = Mathf.FloorToInt(brushSize * paintTexture.width);

        //Paint circular brush
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                //Check if the pixel lies within the circular radius
                if (i * i + j * j <= radius * radius)
                {
                    int px = x + i;
                    int py = y + j;

                    //Ensure pixel is within texture bounds
                    if (px >= 0 && px < paintTexture.width && py >= 0 && py < paintTexture.height)
                    {
                        //Check if the pixel is already painted
                        if (!paintedPixels[px, py]) //Only count unpainted pixels
                        {
                            paintedPixels[px, py] = true; //Mark the pixel as painted
                            totalPaintedPixels++;         //Increase unique painted pixel count
                        }

                        //Set pixel color with brushColor
                        tempTexture.SetPixel(px, py, brushColor);
                    }
                }
            }
        }

        //Apply changes to tempTexture and update the RenderTexture
        tempTexture.Apply();
        Graphics.Blit(tempTexture, paintTexture);

        RenderTexture.active = null;
    }

    private void CalculatePaintedPercentage()
    {
        //Calculate the painted percentage
        paintPercent = Mathf.Round((float)totalPaintedPixels / totalPixels * 100);
        Debug.Log("Painted Percentage: " + paintPercent + "%");

        if(paintPercent >= 100)
        {
            GameManager.Instance.GameLevelComplete();
        }
    }
}
