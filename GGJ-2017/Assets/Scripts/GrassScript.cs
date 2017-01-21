using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrassScript : MonoBehaviour {

	bool needRotation = false;
	bool rotateToRight = false;
	Quaternion lastRotation;
	Quaternion startingRotation;
	Quaternion rotatedRightRotation;
	Quaternion rotatedLeftRotation;
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
		startingRotation = transform.rotation;
		rotatedRightRotation.eulerAngles = transform.rotation.eulerAngles - new Vector3 (0, 60, 0);
		rotatedLeftRotation.eulerAngles = transform.rotation.eulerAngles + new Vector3 (0, 60, 0);
		Debug.Log (rotatedLeftRotation + " " + startingRotation);
		lastRotation = startingRotation;
	}
	
	// Update is called once per frame
	void Update () {
		rotationTimer += Time.deltaTime;
		if (needRotation) {
			if (rotateToRight) {
				transform.rotation = Quaternion.Lerp (lastRotation, rotatedRightRotation, rotationTimer * 4f);
			} else {
				transform.rotation = Quaternion.Lerp (lastRotation, rotatedLeftRotation, rotationTimer * 4f);
			}
		} else {
			transform.rotation = Quaternion.Lerp (lastRotation, startingRotation, rotationTimer);
		}
	}

    float GetAngleFromBase(Vector3 pos)
    {
        return Mathf.Atan2(pos.z, pos.x) * Mathf.Rad2Deg;
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Enemy") {
			numberOfCollisions++;
			var otherAngle = GetAngleFromBase (other.transform.position);
			var grassAngle = GetAngleFromBase (transform.position);
			if (otherAngle > grassAngle)
				rotateToRight = true;
			else
				rotateToRight = false;
			if (!needRotation) {
				lastRotation = transform.rotation;
				rotationTimer = 0;
			}
			needRotation = true;
		}
    }

    void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy") {
			numberOfCollisions--;
			if (numberOfCollisions == 0){
				lastRotation = transform.rotation;
				rotationTimer = 0;
			}
			needRotation = false;
		}
    }
}
