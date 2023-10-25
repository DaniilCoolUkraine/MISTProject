using UnityEngine;

namespace MistProject.General
{
    public class SpriteHolder
    {
        public Sprite Sprite { get; private set; }
        
        public void SetSprite(Sprite sprite)
        {
            if (Sprite != null)
            {
                Dispose();
            }

            Sprite = sprite;
        }

        public void Dispose()
        {
            Object.Destroy(Sprite);
        }
    }
}