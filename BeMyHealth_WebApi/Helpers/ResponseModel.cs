namespace BeMyHealth_WebApi.Helpers
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public bool Error { get; set; }


        public ResponseModel(object data, int statusCode, string statustext, bool error)
        {
            Data = data;
            Status = statusCode;
            StatusText = statustext;
            Error = error;
        }
    }
}
