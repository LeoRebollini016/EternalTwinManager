using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Infrastructure.External.Brute.Response;

namespace EternalTwinManager.Infrastructure.External.Brutes.Mappers;

public static class FightMapperExtension
{
    public static FightDto ToDto(this FightResponse fightResult)
    {
        return new FightDto(
            Executed: true,
            RemainingFights: fightResult.FightsLeft
        );
    }
}
