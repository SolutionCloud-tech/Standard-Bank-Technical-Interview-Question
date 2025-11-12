using MediatR;
using InterviewApp.Services;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Requests
{
    public class GreetUserHandler : IRequestHandler<GreetUserCommand, string>
    {
        private readonly IGreetingService _greetingService;
        private readonly ILogger<GreetUserHandler> _logger;

        public GreetUserHandler(IGreetingService greetingService, ILogger<GreetUserHandler> logger)
        {
            _greetingService = greetingService;
            _logger = logger;
        }

        public Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing GreetUserCommand via MediatR...");
            _greetingService.Run();
            return Task.FromResult("Greeting executed successfully.");
        }
    }
}
