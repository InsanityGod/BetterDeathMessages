using BetterDeathMessages.Code.Behaviors;
using Vintagestory.API.Common;

namespace BetterDeathMessages.Code;

public static class Extensions
{
    /// <summary>
    /// Used to temporary override the pool of death messages (is automatically cleared after next damage is applied)
    /// </summary>
    public static void OverrideDeathMessagePool(this EntityPlayer entityPlayer, string poolIdentifier)
    {
        var provider = entityPlayer.GetBehavior<DeathMessageProvider>();
        if(provider == null)
        {
            entityPlayer.Api?.Logger.Warning("[BetterDeathMessages] Player does not have a DeathMessageProvider assigned?");
            return;
        }

        var pool = DeathMessagePool.Pools.Find(pool => pool.PoolIdentifier == poolIdentifier);
        if(pool is null) entityPlayer.Api?.Logger.Warning("[BetterDeathMessages] no pool with '{0}' as PoolIdentifier", poolIdentifier);

        provider.CustomDeathMessagePool = pool;
    }

    /// <summary>
    /// Used to temporary override the language string code of death messages (is automatically cleared after next damage is applied)
    /// </summary>
    public static void OverrideDeathMessageCode(this EntityPlayer entityPlayer, string code)
    {
        var provider = entityPlayer.GetBehavior<DeathMessageProvider>();
        if(provider == null)
        {
            entityPlayer.Api?.Logger.Warning("[BetterDeathMessages] Player does not have a DeathMessageProvider assigned?");
            return;
        }
        provider.CustomDeathMessageCode = code;
    }
}
