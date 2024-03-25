using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Assessment.Repository

{
    public class DataStore
    {
        private static int Uid = 1001;
        private static int Tid = 101;

        private List<UserModel> _users = new List<UserModel>();
        private List<TrainingModel> _trainings = new List<TrainingModel>();
        private List<EnrollModel> _enrolled = new List<EnrollModel>();
        public List<UserModel> UserData
        {
            get
            {
                return _users;
            }
        }

        public List<TrainingModel> TrainingData
        {
            get
            {
                return _trainings;
            }
        }

        public List<EnrollModel> EnrollData
        {
            get
            {
                return _enrolled;
            }
        }
        

        public DataStore()
        {
            FillDummyData(); 
        }
        public void AddNewUser(string FName, string LName, string Email, string Pswd, Roles Role)
        {
            UserModel Temp = new UserModel(Uid++, FName, LName,  Email, Pswd, Role);
            _users.Add(Temp);
        }

        public void UpdateUser(UserModel Updateduser)
        {
            // Console.WriteLine("Uid " + UserId + "  Tid " + TrainId);
            for (int i = _users.Count - 1; i >= 0; i--)
            {
                UserModel U = _users[i];
                if (U.Uid == Updateduser.Uid)
                {
                    U.FName = Updateduser.FName;
                    U.LName = Updateduser.LName;
                    U.Email = Updateduser.Email;
                    U.Pswd = Updateduser.Pswd;
                    U.Role = Updateduser.Role;
                    return;
                }
            }
        }
        public void AddNewTraining(string TrainingName, DateTime Time, string Department, Modes TrainingModes, Status TrainingStatus)
        {
            TrainingModel Temp = new TrainingModel(Tid++,TrainingName,Time,Department,TrainingModes,TrainingStatus);
            _trainings.Add(Temp);
        }

        public void UpdateTraining(TrainingModel Updatedtraining)
        {
            // Console.WriteLine("Uid " + UserId + "  Tid " + TrainId);
            for (int i = _trainings.Count - 1; i >= 0; i--)
            {
                TrainingModel T = _trainings[i];
                if (T.Tid == Updatedtraining.Tid)
                {
                    T.TrainingName = Updatedtraining.TrainingName;
                    T.Department = Updatedtraining.Department;
                    T.TrainingModes = Updatedtraining.TrainingModes;
                    T.TrainingStatus = Updatedtraining.TrainingStatus;

                    return;
                }
            }
        }
        public void AddNewEnrolled(int UserId, int TrainId)
        {
            EnrollModel Temp = new EnrollModel( UserId, TrainId);
            _enrolled.Add(Temp);
        }

        public void DeleteEnrolled(int UserId, int TrainId)
        {
           // Console.WriteLine("Uid " + UserId + "  Tid " + TrainId);
            for (int i = _enrolled.Count - 1; i >= 0; i--)
            {
                EnrollModel E = _enrolled[i];
                if (E.UserId == UserId && E.TrainId == TrainId )
                {
                    _enrolled.RemoveAt(i);
                }
            }
        }
        public void DeleteTraining(int TrainId)
        {
            // Console.WriteLine("Uid " + UserId + "  Tid " + TrainId);
            for (int i = _trainings.Count - 1; i >= 0; i--)
            {
                TrainingModel T = _trainings[i];
                if (T.Tid == TrainId)
                {
                    _trainings.RemoveAt(i);
                }
            }
        }
        public void DeleteUser(int UserId)
        {
            // Console.WriteLine("Uid " + UserId + "  Tid " + TrainId);
            for (int i = _users.Count - 1; i >= 0; i--)
            {
                UserModel U = _users[i];
                if (U.Uid == UserId)
                {
                    _users.RemoveAt(i);
                }
            }
        }



        public void FillDummyData()
        {
            UserModel TempU;
            TempU = new UserModel(Uid++, "Pulkit", "Tyagi", "tyagi@gmail.com", "pulkit", Roles.Admin);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "John", "Doe", "john.doe@example.com", "john", Roles.Trainer);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Jane", "Smith", "jane.smith@example.com", "jane", Roles.Trainer);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Alice", "Johnson", "alice.johnson@example.com", "alice", Roles.Trainer);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Bob", "Brown", "bob.brown@example.com", "bob", Roles.Trainee);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Emily", "Davis", "emily.davis@example.com", "emily", Roles.Trainee);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Michael", "Wilson", "michael.wilson@example.com", "michael", Roles.Trainee);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Sarah", "Taylor", "sarah.taylor@example.com", "sarah", Roles.Trainee);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "David", "Clark", "david.clark@example.com", "david", Roles.Trainee);
            _users.Add(TempU);
            TempU = new UserModel(Uid++, "Emma", "Martinez", "emma.martinez@example.com", "emma", Roles.Trainee);
            _users.Add(TempU);


            TrainingModel TempT;
            TempT = new TrainingModel(Tid++, "Quantum Computing", DateTime.Now, "PCM1", Modes.Online, Status.Scheduled);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "AI & ML", DateTime.Now, "PCM2", Modes.Online, Status.YetToStart);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "5G Technology", DateTime.Now, "PCM1", Modes.Online, Status.Assessment);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Internet of Things", DateTime.Now, "PCM2", Modes.Offline, Status.In_Progress);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Augmented Reality", DateTime.Now, "PCM12", Modes.Offline, Status.Scheduled);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Virtual Reality ", DateTime.Now, "PCM1", Modes.Offline, Status.Scheduled);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Blockchain Technology", DateTime.Now, "PCM12", Modes.Online, Status.Assessment);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Edge Computing", DateTime.Now, "PCM12", Modes.Online, Status.YetToStart);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Autonomous Vehicles", DateTime.Now, "PCM12", Modes.Online, Status.Scheduled);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Robotics and Automation", DateTime.Now, "PCM12", Modes.Online, Status.In_Progress);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Biotechnology", DateTime.Now, "PCM12", Modes.Online, Status.Scheduled);
            _trainings.Add(TempT);
            TempT = new TrainingModel(Tid++, "Genetic Engineering", DateTime.Now, "PCM12", Modes.Online, Status.Feedback);
            _trainings.Add(TempT);

            EnrollModel TempE;
             TempE = new EnrollModel(1001, 101);
             _enrolled.Add(TempE);
            TempE = new EnrollModel(1005, 101);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1006, 101);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1007, 101);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1003, 101);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1004, 102);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1005, 103);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1009, 101);
            _enrolled.Add(TempE);
            TempE = new EnrollModel(1010, 102);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1001, 103);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1002, 104);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1002, 105);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1002, 106);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1003, 107);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1003, 108);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1003, 109);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1004, 110);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1004, 111);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1004, 112);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1005, 101);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1005, 102);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1005, 103);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1006, 104);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1007, 105);
             _enrolled.Add(TempE);
             TempE = new EnrollModel(1008, 106);
             _enrolled.Add(TempE);
             

        }
    }
}
