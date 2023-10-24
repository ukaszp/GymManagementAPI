namespace GymApi.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WhenStarts { get; set; }
        public int DurationInMinutes { get; set; }
        public virtual List<User> Participants { get; set; }
        public virtual User Trainer { get; set; }
    }
}
