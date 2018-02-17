using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour {
    Camera cam;
    public int ppu = 128;
    public int scale = 1;
    
	void Start () {
        Resize();
	}

    public void Resize()
    {
        cam = gameObject.GetComponent<Camera>();

        //Debug.Log("Screen Resolution: " + cam.pixelWidth + " x " + cam.pixelHeight + ".");

        float maxTileHeight = (float)cam.pixelHeight / (float)(ppu * scale);
        //Debug.Log("unit height: " + maxTileHeight);
        cam.orthographicSize = maxTileHeight * 0.5f;
    }
}
