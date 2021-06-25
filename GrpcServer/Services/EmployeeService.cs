using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcEmployee
{
    public class EmployeeService : Employee.EmployeeBase
    {
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger)
        {
            _logger = logger;
        }

        public override Task<EmployeeInfoResponse> GetEmployeeInfo(EmployeeInfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EmployeeInfoResponse
            {
                Employee = new EmployeeInfo
                {
                    ID = 100,
                    FirstName = "Rollin",
                    LastName = "Nolan",
                    Address = "MySpace",
                    PhoneNumber = "111-111-1100",
                    EmailAddress = "rollin.nolan@myspace.com"
                },
                Status = new GStatus
                {
                    Successfull = false,
                    ErrorCode = 0,
                    ErrorMessage = string.Empty
                }
            });
        }
    }
}
