using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.DeathMessageGeneration.CustomPools;

public class SelfProjectileDeathMessagePool : DeathMessagePool
{

    public override bool IsApplicable(ConnectedClient client, DamageSource src)
    {
        if(!base.IsApplicable(client, src)) return false;
        if(client.Entityplayer != src.CauseEntity) return false;
        return src.SourceEntity is EntityProjectile or EntityThrownStone;
    }
}
