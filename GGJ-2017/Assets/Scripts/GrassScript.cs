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
            var spriteRenderer = this.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[generator.Next(sprites.Count())];
        }          
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
