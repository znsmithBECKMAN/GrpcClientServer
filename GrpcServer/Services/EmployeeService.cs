using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcEmployee
{
    public class EmployeeService : Employee.EmployeeBase
    {
        private Dictionary<int, EmployeeInfo> _employees;

        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger)
        {
            _logger = logger;
            _employees = new Dictionary<int, EmployeeInfo>();

            _employees[100] = new EmployeeInfo()
            {
                ID = 100,
                FirstName = "Rollin",
                LastName = "Nolan",
                Address = "MySpace",
                PhoneNumber = "111-111-1100",
                EmailAddress = "rollin.nolan@myspace.com"
            };
        }

        public override Task<EmployeeInfoResponse> GetEmployeeInfo(EmployeeInfoRequest request, ServerCallContext context)
        {
            var response = new EmployeeInfoResponse();
            var status = new GStatus();
            if (_employees.TryGetValue(request.ID, out EmployeeInfo employee))
            {
                status.Successfull = true;
                response.Employee = employee;
            }
            else
            {
                status.Successfull = false;
                status.ErrorCode = 1;
                status.ErrorMessage = $"Employee ID {request.ID} not found.";
            }
            response.Status = status;
            return Task.FromResult(response);
        }
    }
}
