using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyCashFlow
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICashRecordService" in both code and config file together.
    [ServiceContract]
    public interface ICashRecordService
    {
        [OperationContract]
        string Register(string fullName, string email, string password);

        [OperationContract]
        string Login(string email, string password);

        [OperationContract]
        void AddRecord(string desc, int amount, string email);

        [OperationContract]
        void DeleteRecord(int id);

        [OperationContract]
        void UpdateRecord(int id, string description, int amount);

        [OperationContract]
        List<Finance> GetAllRecords(string email);
    }
}
