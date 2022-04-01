namespace WebAPI_Projekt.Models
{
    public class GeoMessage
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        public GeoMessage ToModel(User user)
        {
            return new GeoMessage
            {
                Title = this.Message.Split(new[] { '.' }).FirstOrDefault(),
                Author = $"{user.FirstName} {user.LastName}",
                Body = this.Message,
                Longitude = this.Longitude,
                Latitude = this.Latitude,
            };
        }
    }
}
