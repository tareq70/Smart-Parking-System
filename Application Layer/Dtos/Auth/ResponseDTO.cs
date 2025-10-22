namespace Smart_Parking_System.Application_Layer.Dtos.Auth
{
    public class ResponseDTO
    {

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }


    }
}
