using MistProject.General;

namespace MistProject.Requests.Response
{
    public class ImageResponseData : IResponseData
    {
        public TextureHolder Texture;
        public byte[] Data { get; set; }
        public long ResponseCode { get; set; }
    }
}