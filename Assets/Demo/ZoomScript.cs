using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ZoomScript : MonoBehaviour
{
    public Image image;
    
    private bool _isZoomed = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomToggle()
    {
        if (_isZoomed)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        _isZoomed = !_isZoomed;
    }

    private void OnPostRender()
    {
        var width = Screen.width / 2;
        var height = Screen.height / 2;
        var startX = Screen.width / 4;
        var startY = Screen.height / 4;
        var tex = ScreenCapture.CaptureScreenshotAsTexture();

        Rect rex = new Rect(startX, startY, width, height);
     
        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        image.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), Vector2.zero);

        Destroy(tex);
    }
}
