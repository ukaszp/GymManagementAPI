namespace GymApi.Models
{
    public class CreateTrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WhenStarts { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
