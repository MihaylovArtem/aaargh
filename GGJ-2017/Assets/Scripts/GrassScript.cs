using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrassScript : MonoBehaviour {

	bool needRotation = false;
	bool rotateToRight = false;
	float rotationTimer = 1f;
	private float rotationAngle = Mathf.PI/3f;
	int numberOfCollisions;
    enum Distination
    {
        Right,
        Left
    };

    public Sprite[] sprites;

    private Distination dist;

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
		if (rotationTimer < 0.4f) {
			rotationTimer += Time.deltaTime;
			transform.RotateAround (transform.position, Vector3.down, (rotateToRight ? -1 : 1) * (needRotation ? -1 : 1) * 100 * Time.deltaTime);
		}
	}

    float GetAngleFromBase(Vector3 pos)
    {
        return Mathf.Atan2(pos.z, pos.x) * Mathf.Rad2Deg;
    }

    void OnTriggerEnter(Collider other)
    {
		Debug.Log ("Trigger Enter");
		if (other.tag == "Enemy") {
			numberOfCollisions++;
			rotationTimer = 0;
			var otherAngle = GetAngleFromBase (other.transform.position);
			var grassAngle = GetAngleFromBase (transform.position);
			if (otherAngle > grassAngle)
				rotateToRight = true;
			else
				rotateToRight = false;
			Debug.Log (otherAngle.ToString () + " other and grass " + grassAngle.ToString ());
			needRotation = true;
		}
    }

    void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy") {
			numberOfCollisions--;
			needRotation = false;
			rotationTimer = 0;
		}
    }
}
