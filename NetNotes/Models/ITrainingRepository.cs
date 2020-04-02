using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetNotesApp.Models
{
     public interface ITrainingRepository
    {
        IEnumerable<Training> GetTrainings();
        List<string> GetInstructor();
        Training GetTrainingByID(int id);
        void InsertTraining(Training training);
        void DeleteTraining(int id);
        void EditTraining(Training training);
    }
}