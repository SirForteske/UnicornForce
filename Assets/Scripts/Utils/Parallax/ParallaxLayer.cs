using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.Parallax
{
    public class ParallaxLayer: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The relative speed this layer will scroll.")]
        private float speed = 0f;
        [Header("Layer Sequence")]
        [Tooltip("If true, the layer sequence will be randomized and won't follow the sequence list order.")]
        [SerializeField]
        private bool randomize = false;
        [Tooltip("The minimum distance between two sprites of this layer.")]
        [SerializeField]
        private float minSpriteDistance = 0.5f;
        [Tooltip("The maximum distance between two sprites of this layer.")]
        [SerializeField]
        private float maxSpriteDistance = 1f;
        [Tooltip("The area, in world space, where the sprites of this layer can spawn.")]
        [SerializeField]
        private Rect spawnArea;
        [Tooltip("How many sprites will be permanently instantiated on this layer.")]
        [SerializeField]
        private int maxActiveSprites;
        [Tooltip("The sequence of sprites that will appear on this layer, one after the other.")]
        [SerializeField]
        private List<Sprite> spritePool;

        private int currentSequenceIndex;
        private List<GameObject> activeSprites = new();
        private SpriteRenderer lastSprite;

        void Start()
        { 
            currentSequenceIndex = -1;

            if (spritePool != null && spritePool.Count > 0)
            {
                for (int i = 0; i < maxActiveSprites; i++)
                {
                    var sprite = new GameObject();
                    sprite.AddComponent<SpriteRenderer>().sprite = spritePool[NextSequenceIndex()];
                    var bounds = sprite.GetComponent<SpriteRenderer>().bounds;
                    sprite.transform.parent = transform;
                    sprite.transform.position = new Vector3(
                        lastSprite == null ? spawnArea.x : lastSprite.bounds.max.x + bounds.extents.x + UnityEngine.Random.Range(minSpriteDistance, maxSpriteDistance),
                        UnityEngine.Random.Range(spawnArea.y, spawnArea.yMax),
                        transform.position.z
                        );

                    lastSprite = sprite.GetComponent<SpriteRenderer>();
                    activeSprites.Add(sprite);
                }
            }
        }

        private void Update()
        {
            Move(1f);
        }

        public void Move(float speedMultiplier)
        {
            var sprites = new List<GameObject>(activeSprites);
            foreach (GameObject sprite in sprites)
            {
                sprite.transform.position -= speed * speedMultiplier * Time.deltaTime * Vector3.right;

                if(Camera.main.WorldToScreenPoint(sprite.GetComponent<SpriteRenderer>().bounds.max).x < 0)
                {
                    SpriteOutsideScreen(sprite);
                }
            }
        }

        private void SpriteOutsideScreen(GameObject sprite)
        {
            if(randomize)
            {
                Destroy(sprite);
                activeSprites.Remove(sprite);

                sprite = new GameObject();
                sprite.AddComponent<SpriteRenderer>().sprite = spritePool[NextSequenceIndex()];
                sprite.transform.parent = transform;

                activeSprites.Add(sprite);
            }

            sprite.transform.position = new Vector3(
                    lastSprite.bounds.max.x + sprite.GetComponent<SpriteRenderer>().bounds.extents.x + UnityEngine.Random.Range(minSpriteDistance, maxSpriteDistance),
                    UnityEngine.Random.Range(spawnArea.y, spawnArea.yMax),
                    transform.position.z
                    );

            lastSprite = sprite.GetComponent<SpriteRenderer>();
        }

        private int NextSequenceIndex()
        {
            if (randomize)
            {
                currentSequenceIndex = UnityEngine.Random.Range(0, spritePool.Count);
            }
            else if(currentSequenceIndex == spritePool.Count - 1)
            {
                currentSequenceIndex = 0;
            }
            else
            {
                currentSequenceIndex++;
            }

            return currentSequenceIndex;
        }
    }
}
