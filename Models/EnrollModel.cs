using System;

namespace Assessment.Models
{
    public class EnrollModel
    {
        public int TrainId { get; set; }
        public int UserId { get; set; }


        public EnrollModel(int UserId, int TrainId) { 
            this.UserId = UserId;
            this.TrainId = TrainId;
        }
        public EnrollModel()
        {}
    }
}
