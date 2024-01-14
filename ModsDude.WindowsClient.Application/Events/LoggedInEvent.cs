using MediatR;

namespace ModsDude.WindowsClient.Application.Events;
public record LoggedInEvent(string Name) : INotification;
