using EternalTwinManager.Core.Brute.Dtos;
using EternalTwinManager.Infrastructure.External.Brute.Response;

namespace EternalTwinManager.Infrastructure.External.Brute.Mappers;

public static class OpponentMapper
{
    public static OpponentDto ToDto(this OpponentResponse r)
        => new(
            r.Name,
            r.Level,
            r.Hp,
            r.EnduranceValue,
            r.StrengthValue,
            r.SpeedValue,
            r.AgilityValue,
            r.Skills,
            r.Weapons,
            r.Pets
        );
}
