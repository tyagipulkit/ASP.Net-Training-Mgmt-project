using System.ComponentModel.DataAnnotations;
using System;

namespace Assessment.Models
{
 
        public enum Status
        {
            YetToStart = 1,
            Scheduled = 2,
            In_Progress = 3,
            Completed = 4,
            Assessment = 5,
            Feedback = 6
        }

        public enum Modes
        {
            Online = 1,
            Offline = 2
        }

    public class TrainingModel
    {

        [Key]
        public int Tid { get; set; }
        public string TrainingName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Department { get; set; }
        public Modes TrainingModes { get; set; }
        public Status TrainingStatus { get; set; }

        public TrainingModel(int Tid, string TrainingName, DateTime Time, string Department, Modes TrainingModes, Status TrainingStatus)
        {
            this.Tid = Tid;
            this.TrainingName = TrainingName;
            this.CreatedDate = Time;
            this.Department = Department;
            this.TrainingModes = TrainingModes;
            this.TrainingStatus = TrainingStatus;
            

        }
        public TrainingModel()
        { }

    }
}
