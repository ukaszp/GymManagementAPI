using GymApi.Entities;

namespace GymApi.Services
{
    public interface ITrainingService
    {
        int CreateTraining(Training training);
        void DeleteTraining(int id);
        IEnumerable<Training> GetAll();
        Training GetById(int id);
        void UpdateTraining(int id, Training training);
    }
}