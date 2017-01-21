using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrassScript : MonoBehaviour {

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
        var generator = new System.Random();
        foreach (var sprite in sprites)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[generator.Next(sprites.Count())];
        }          
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float GetAngleFromBase(Vector3 pos)
    {
        return Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
    }

    void OnTriggerEnter(Collider other)
    {
        var otherAngle = GetAngleFromBase(other.transform.position);
        var grassAngle = GetAngleFromBase(transform.position);

        if (otherAngle > grassAngle)
            transform.RotateAround(transform.position, Vector3.down, 30 * Time.deltaTime);
        else
            transform.RotateAround(transform.position, Vector3.down, -30 * Time.deltaTime);
    }

    void OnTriggerExit(Collider other)
    {

    }
}
