using EternalTwinManager.Core.Dtos.Brutes.Shared;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.Login;

public record LoginCommandRequest(string Username, string Password) : IRequest<LoginResultDto?>;