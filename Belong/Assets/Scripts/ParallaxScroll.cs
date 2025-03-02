using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{

    public float backgroundSize;
    public float parallaxSpeed;
    public float parallaxSpeedY;

    public int sortOrder;
    public Sprite sprite;

    private Transform cameraTransform;

    private GameObject emptyGameObject;
    private Transform[] layers;
    private Transform[] layersY;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private int leftIndexY;
    private int rightIndexY;

    private float lastCameraX;
    private float lastCameraY;


    void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        layers = new Transform[3];
        layersY = new Transform[3];

        for (int i = 0; i < 3; i++)
        {
            GameObject go = new GameObject(); 
            go.transform.position = transform.position + new Vector3((i - 1) * backgroundSize, 0, 0);
            go.transform.parent = transform;
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortOrder;
            spriteRenderer.sortingLayerName = "Other";
            go.name = i.ToString();
            layers[i] = go.transform;
           
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject go2 = new GameObject(); 
            go2.transform.position = transform.position + new Vector3(0, (i - 1) * backgroundSize, 0);
            go2.transform.parent = transform;
            SpriteRenderer spriteRenderer = go2.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortOrder;
            spriteRenderer.sortingLayerName = "Other";
            go2.name = i.ToString();
            layersY[i] = go2.transform;
           
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        leftIndexY = 0;
        rightIndexY = layersY.Length - 1;
        
    }

    private void ScrollLeft()
    {
        layers[rightIndex].localPosition = Vector3.right * (layers[leftIndex].localPosition.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        layers[leftIndex].localPosition = Vector3.right * (layers[rightIndex].localPosition.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }

    }

    private void ScrollUp(){
        layersY[rightIndexY].localPosition = Vector3.up * (layersY[leftIndexY].localPosition.y - backgroundSize);
        leftIndexY = rightIndexY;
        rightIndexY--;
        if (rightIndexY < 0)
        {
            rightIndexY = layersY.Length - 1;
        }
        
    }
    private void ScrollDown(){
        layersY[leftIndexY].localPosition = Vector3.up * (layersY[rightIndexY].localPosition.y + backgroundSize);
        rightIndexY = leftIndexY;
        leftIndexY++;
        if (leftIndexY == layersY.Length)
        {
            leftIndexY = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        float deltaY  = cameraTransform.position.y - lastCameraY;
        transform.position += Vector3.up * (deltaY * parallaxSpeedY);
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
        if (cameraTransform.position.y < (layersY[leftIndexY].transform.position.y + viewZone))
        {
            ScrollDown();
        }
        if (cameraTransform.position.y > (layersY[rightIndexY].transform.position.y - viewZone))
        {
            ScrollUp();
        }
    }
}
