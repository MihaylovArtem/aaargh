using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GrassController : MonoBehaviour {
    public int grassCount = 72;
    public float radius = 100f;
    public GameObject grassObject;

    private System.Random generator;

	// Use this for initialization
	void Start ()
    {
        var angle = 0f;
        var offset = Mathf.PI / grassCount * 2;
        generator = new System.Random();
        for (int i = 0; i < grassCount; i++, angle += offset) {
            var grass = Instantiate(grassObject) as GameObject;
            grass.transform.position = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            grass.transform.LookAt(Vector3.zero);

            grass.transform.localScale += new Vector3(GetRandomNumber(0.8, 1.2), GetRandomNumber(0.8, 1.5), 0);
        }
	}

    private float GetRandomNumber(double minimum, double maximum)
    {
        return (float)(generator.NextDouble() * (maximum - minimum) + minimum);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
