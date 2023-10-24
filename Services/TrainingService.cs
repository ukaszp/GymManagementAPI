using GymApi.Entities;
using GymApi.Exceptions;

namespace GymApi.Services
{
    public class TrainingService
    {
        private readonly GymDbContext _dbContext;
        private readonly ILogger<TrainingService> _logger;

        public TrainingService(GymDbContext dbContext, ILogger<TrainingService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Training GetById(int id)
        {
            var training= _dbContext
                .Trainings
                .FirstOrDefault(x => x.Id == id);

            if(training == null)
            {
                throw new NotFoundException("Training not found");
            }

            return training;
        }

        public IEnumerable<Training> GetAll()
        {
            var trainings = _dbContext
                .Trainings
                .ToList();

            return trainings;
        }

        public int CreateTraining(Training training)
        {
            _dbContext.Trainings.Add(training);
            _dbContext.SaveChanges();

            return training.Id;
        }

        public void DeleteTraining(int id)
        {
            _logger.LogWarning($"Training with id: {id} DELETE action invoked");

            var training = _dbContext
                .Trainings
                .FirstOrDefault(u => u.Id == id);

            if (training is null)
                throw new NotFoundException("Training not found");

            _dbContext.Trainings.Remove(training);
            _dbContext.SaveChanges();
        }

        public void UpdateTraining(int id, Training training)
        {
            var trainingdb = _dbContext
              .Trainings
              .FirstOrDefault(u => u.Id == id);

            if (training is null)
                throw new NotFoundException("Training not found");

            trainingdb.Name = training.Name;
            trainingdb.Description = training.Description;
            trainingdb.WhenStarts = training.WhenStarts;
            trainingdb.DurationInMinutes = training.DurationInMinutes;
            trainingdb.Trainer = training.Trainer;

            _dbContext.SaveChanges();
        }
    }
}
