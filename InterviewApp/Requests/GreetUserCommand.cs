using MediatR;

namespace InterviewApp.Requests
{
    public class GreetUserCommand : IRequest<string>
    {
    }
}