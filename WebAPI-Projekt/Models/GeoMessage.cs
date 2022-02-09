namespace WebAPI_Projekt.Models
{
    public class GeoMessage
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
