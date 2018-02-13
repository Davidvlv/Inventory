using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour {
    Camera cam;
    public int ppu = 128;
    public int scale = 1;

	// Use this for initialization
	void Start () {
        cam = gameObject.GetComponent<Camera>();

        // set orthographic size to pixel perfect
        // Orthographic size = ((Vert Resolution)/(PPUScale * PPU)) * 0.5
        float size = cam.pixelHeight / (scale * ppu) * 0.5f;
        cam.orthographicSize = size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
