using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.DeathMessageGeneration.CustomPools;

public class ArrowDeathMessagePool : DeathMessagePool
{

    public override bool IsApplicable(ConnectedClient client, DamageSource src)
    {
        if(!base.IsApplicable(client, src)) return false;
        return src.SourceEntity is IProjectile projectile && (projectile.ProjectileStack?.Collectible?.Code.Path.StartsWith("arrow") ?? false);
    }
}
