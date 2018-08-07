using UnityEngine;
using System.Collections.Generic;

namespace Exploder2D.Demo
{
    public class MovingObjects : MonoBehaviour
    {
        public List<GameObject> rows;

        public float Speed = 10.0f;
        public float yStart = 22.5f;
        public float yLimit = 0.3f;

        private void Update()
        {
            foreach (var row in rows)
            {
                row.transform.position -= new Vector3(0.0f, Speed*Time.deltaTime, 0.0f);

                if (row.transform.position.y < yLimit)
                {
                    row.transform.position = new Vector3(row.transform.position.x, yStart);

                    var sprites = row.GetComponentsInChildren<SpriteRenderer>(true);

                    foreach (var sprite in sprites)
                    {
                        Exploder2DUtils.SetActiveRecursively(sprite.gameObject, true);
                    }
                }
            }
        }
    }
}
