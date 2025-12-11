using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EternalTwinManager.Core.Dtos.Brutes.Battles;

public record FightDto(
    bool Executed,
    int? RemainingFights
);