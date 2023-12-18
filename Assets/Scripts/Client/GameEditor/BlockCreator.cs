using System.Collections.Generic;
using UnityEngine;

namespace Client.GameEditor
{
    public class BlockCreator : MonoBehaviour
    {
        public Vector2[] attachPoints; //TODO this currently does not correspond to the sprite marker for attachpoint and needs to be initalized with the object 
        //public Sprite ConnectionPointSprite; **deprecated**
        //TODO add connection point sprite to each attach point
        public void Start()
        {
            // Get the sprite renderer and the sprite
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;

            // Initialize the attachPoints list
            attachPoints = new List<Vector2>();

            // Add attach points at each corner of the sprite
            float halfWidth = sprite.bounds.size.x / 2;
            float halfHeight = sprite.bounds.size.y / 2;
            attachPoints.Add(new Vector2(-halfWidth, -halfHeight)); // Bottom left corner
            attachPoints.Add(new Vector2(halfWidth, -halfHeight)); // Bottom right corner
            attachPoints.Add(new Vector2(-halfWidth, halfHeight)); // Top left corner
            attachPoints.Add(new Vector2(halfWidth, halfHeight)); // Top right corner
        }
    }
}