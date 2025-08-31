using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.DeathMessageGeneration.CustomPools;

public class ArrowDeathMessagePool : DeathMessagePool
{

    public override bool IsApplicable(ConnectedClient client, DamageSource src)
    {
        if(!base.IsApplicable(client, src)) return false;
        return src.SourceEntity is EntityProjectile projectile && projectile.Code.Path.StartsWith("arrow");
    }
}
