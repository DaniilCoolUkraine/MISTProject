namespace MistProject.Requests.Response
{
    public interface IResponseData
    {
        public byte[] Data { get; set; }
        public long ResponseCode { get; set; }
    }
}