using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Senwes.DAL.Model;

namespace Senwes.DAL.Data
{
    public class LoadData
    {
        private List<Employee> _empData;

        public LoadData()
        {
            _empData = null;
        }

        public IEnumerable<Employee> LoadEmployeeData()
        {
            if (_empData == null)
            {
                var jsonFilePath = @"..\Senwes.DAL\Data\Employee.json";
                var json = File.ReadAllText(jsonFilePath);

                _empData = JsonConvert.DeserializeObject<List<Employee>>(json);
            }

            return _empData;
        }
    }
}
