namespace MistProject.Requests.Response
{
    public class ErrorResponseData : IResponseData
    {
        public byte[] Data { get; set; }
        public long ResponseCode { get; set; }
    }
}