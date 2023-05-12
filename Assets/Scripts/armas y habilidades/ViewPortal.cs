using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewPortal : MonoBehaviour
{
    Camera cameraPortal;
    Camera cameraMain;
    RenderTexture tempTexture;
    private void Awake()
    {
        cameraMain = GetComponent<Camera>();
        tempTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }
    void Start()
    {
        GetComponent<Material>().mainTexture = tempTexture;
    }
    private void OnEnable()
    {

    }


    void Update()
    {

    }
}