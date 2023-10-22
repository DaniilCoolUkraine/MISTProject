namespace MistProject.Requests.Post
{
    public interface IPostData
    {
        public string JsonData { get; set; }
        public byte[] RawData { get; set; }
    }
}