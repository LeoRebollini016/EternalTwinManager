using EternalTwinManager.Core.Brute.Dtos.Auth;
using EternalTwinManager.Infrastructure.External.Brute.Response;

namespace EternalTwinManager.Infrastructure.External.Brute.Mappers;

public static class AuthenticateMapperExtension
{
    public static AuthUserDto ToDto(this AuthenticateResponse response)
    {
        var u = response.User;

        return new AuthUserDto(
            UserId: u.Id,
            UserName: u.Name,
            ConnexionToken: u.ConnexionToken,
            Gold: u.Gold,
            BruteLimit: u.BruteLimit,
            LastSeen: u.LastSeen,
            Brutes: u.Brutes!.Select(b => new AuthBruteDto(
                Id: b.Id,
                Name: b.Name,
                Level: b.Level,
                Ranking: b.Ranking,
                FightsLeft: b.FightsLeft,
                Victories: b.Victories,
                Losses: b.Losses,
                Favorite: b.Favorite,
                Gender: b.Gender,
                Body: b.Body,
                Colors: b.Colors
            )).ToList()
        );
    }
}
