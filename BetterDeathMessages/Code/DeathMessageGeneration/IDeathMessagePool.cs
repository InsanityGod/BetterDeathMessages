using BetterDeathMessages.Code.Behaviors;
using Vintagestory.API.Common;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.DeathMessageGeneration;

public interface IDeathMessagePool
{
    /// <summary>
    /// Identifier of the pool, this should be unique as it is used by <see cref="Extensions.OverrideDeathMessagePool"/> to manually set <see cref="DeathMessageProvider.CustomDeathMessagePool"/>
    /// </summary>
    string PoolIdentifier { get; }

    /// <summary>
    /// Provides a random death message
    /// </summary>
    string GetRandomMessage(ConnectedClient client, DamageSource src);

    /// <summary>
    /// Wether this pool can/should be used
    /// </summary>
    bool IsApplicable(ConnectedClient client, DamageSource src);
}
