using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject terrainObject;
    public Sprite[] sprites;
    public int numberOfObjects = 50;

    void Start ()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var terrain = Instantiate(terrainObject) as GameObject;          
            var angle = Random.Range(0f, Mathf.PI * 2);

            var spriteRenderer = terrain.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            var xOffset = (Mathf.Cos(angle) < 0) ? -Random.Range(0f, 30f) : Random.Range(0f, 30f);
            var yOffset = (Mathf.Sin(angle) < 0) ? -Random.Range(0f, 30f) : Random.Range(0f, 30f);

            terrain.transform.position = new Vector3(Mathf.Cos(angle) * 50 + xOffset, 0f, Mathf.Sin(angle) * 50 + yOffset);
            terrain.transform.LookAt(Vector3.zero);
        }        
    }
}
