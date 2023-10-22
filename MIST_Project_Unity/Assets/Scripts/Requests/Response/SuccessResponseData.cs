namespace MistProject.Requests.Response
{
    public class SuccessResponseData : IResponseData
    {
        public byte[] Data { get; set; }
        public long ResponseCode { get; set; }
    }
}