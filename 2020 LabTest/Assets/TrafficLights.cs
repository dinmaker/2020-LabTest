using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public bool isGreen;
    public bool isRed;
    public bool isYellow;

    public int colourSet;

    public float colourTime;
    float lastColourTime; 

    // Start is called before the first frame update
    void Start()
    {
        colourSet = Random.Range(0, 2);
        SetColour();
    }

    // Update is called once per frame
    void Update()
    {
        

        
        
        lastColourTime += Time.deltaTime;

        if (lastColourTime > colourTime)
        {
            colourSet += 1;
            lastColourTime = 0;
            SetColour();
        }
    }

    void SetColour()
    {
        if (colourSet > 2)
        {
            colourSet = 0;
        }

        switch (colourSet)
        {
            case 0:
                isGreen = true;
                isYellow = false;
                isRed = false;
                break;
            case 1:
                isGreen = false;
                isYellow = true;
                isRed = false;
                break;
            case 2:
                isGreen = false;
                isYellow = false;
                isRed = true;
                break;
        }

        if (isGreen == true)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.green);
        }
        if (isRed == true)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.red);
        }
        if (isYellow)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.yellow);
        }

        if (isGreen == true || isRed == true)
        {
            colourTime = Random.Range(5, 10);
        }
        else
        {
            colourTime = 4;
        }
        return;
    }
}
