using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls;

namespace StatisticalTracker.Models
{
    public class ContestModel
    {
        public string EntryId { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public string ContestTitle { get; set; }
        public string SalaryCap { get; set; }
        public double Score { get; set; }
        public double Position { get; set; }
        public double Entries { get; set; }
        public string Opponent { get; set; }
        public double EntryFee { get; set; }
        public double Winnings { get; set; }
        public string Link { get; set; }

        public List<String> Errors { get; set; }
        public MockContestModel Entity { get; set; }
        public bool IsEmpty { get; set; }
        public bool HasErrors { get; set; }
        private DataRow _row { get; set; }

        public ContestModel()
        {
        }

        public ContestModel(DataRow row)
        {            
            _row = row;
            Errors = new List<string>();

            //if (string.IsNullOrEmpty(GetExisting("First_Name"))
            //    || string.IsNullOrEmpty(GetExisting("Last_Name")))
            //{
            //    IsEmpty = true;
            //}

            //else
            //{
            IsEmpty = false;
            Entity = new MockContestModel
            {
                EntryId = "Entry Id",
                Sport = "Sport",
                //Date = "Date",
                //MiddleName =
                //    ValidateField("Middle_Name", PatInfoConstraints.MiddleName, PatientValidatorHelper.IsValidFreeForm),
                //Ssn = ValidateField("SSN", PatInfoConstraints.Ssn, PatientValidatorHelper.IsValidSSN),
                //MaritalStatus = ValidateField("Marital_Status", PatInfoConstraints.MaritalStatus,
                //    PatientValidatorHelper.IsValidMaritalStatus),
                //Birthday = ValidateDateField("Birthday"),
                //Sex = ValidateField("Sex", PatInfoConstraints.Sex, PatientValidatorHelper.IsValidSex),
                //Address1 =
                //    ValidateField("Address_1", PatInfoConstraints.Address1, PatientValidatorHelper.IsValidFreeForm),
                //Address2 =
                //    ValidateField("Address_2", PatInfoConstraints.Address2, PatientValidatorHelper.IsValidFreeForm),
                //City = ValidateField("City", PatInfoConstraints.City, PatientValidatorHelper.IsValidFreeForm),
                //State = ValidateField("State", PatInfoConstraints.State),
                //Zip = ValidateField("Zip", PatInfoConstraints.Zip, PatientValidatorHelper.IsValidZip),
                //Country = ValidateField("Country", PatInfoConstraints.Country, PatientValidatorHelper.IsValidFreeForm),
                //PatPhone = ValidateField("Pat_Phone", PatInfoConstraints.PatPhone,
                //    PatientValidatorHelper.IsValidPhone),
                //CellPhone = ValidateField("Cell_Phone", PatInfoConstraints.CellPhone,
                //    PatientValidatorHelper.IsValidPhone),
                //Email = ValidateField("Email", PatInfoConstraints.Email),
                //LastSvcDate = ValidateDateField("Last_Svc_Date"),
                //LegacyNotes =
                //    ValidateField("Legacy_Notes", PatInfoConstraints.LegacyNotes, PatientValidatorHelper.IsValidFreeForm),
                ////};
                //HasErrors = Errors.Count > 0
            };

        }

        public string StringifyErrors()
        {
            return string.Join(", ", Errors.ToArray());
        }
    }

    public class MockContestModel
    {
        public string EntryId { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public string ContestTitle { get; set; }
        public string SalaryCap { get; set; }
        public double Score { get; set; }
        public int Position { get; set; }
        public int Entries { get; set; }
        public string Opponent { get; set; }
        public double EntryFee { get; set; }
        public double Winnings { get; set; }
        public string Link { get; set; }
    }
}