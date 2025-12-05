using EternalTwinManager.Application.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.Login;

public record LoginCommandRequest(string Username, string Password) : IRequest<BruteLoginResult?>;