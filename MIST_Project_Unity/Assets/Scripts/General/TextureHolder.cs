using UnityEngine;

namespace MistProject.General
{
    public class TextureHolder
    {
        public Texture2D Texture { get; private set; }

        public TextureHolder()
        {
            Texture = null;
        }
        
        public TextureHolder(byte[] rawTexture)
        {
            Texture = new Texture2D(Constants.LOADED_IMAGE_SIZE, Constants.LOADED_IMAGE_SIZE);
            Texture.LoadRawTextureData(rawTexture);
            Texture.Apply();
        }
        
        public void SetTexture(Texture2D texture)
        {
            if (Texture != null)
            {
                Dispose();
            }

            Texture = texture;
        }

        public void Dispose()
        {
            Object.Destroy(Texture);
        }
    }
}