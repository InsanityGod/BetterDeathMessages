using BetterDeathMessages.Code.DeathMessageGeneration;
using System;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.Server;

namespace BetterDeathMessages.Code;

/// <summary>
/// Basic IDeathMessagePool implementation
/// LanguageString is compromnised of <see cref="BaseCode"/> and random number up to <see cref="Length"/> seperated by a '-'
/// </summary>
public partial class DeathMessagePool : IDeathMessagePool
{
    /// <summary>
    /// If enabled this pool won't be randomly selected and has to be manually targeted through calling <see cref="Extensions.OverrideDeathMessagePool"/> before doing damage
    /// </summary>
    public bool ManualAssignmentOnly { get; set; }

    /// <summary>
    /// Filter for if <see cref="DamageSource.CauseEntity"/> is present
    /// </summary>
    public bool? CausedByEntityFilter { get; init; }

    /// <summary>
    /// Filter on <see cref="DamageSource.Source"/>
    /// </summary>
    public EnumDamageSource? DamageSourceFilter { get; init; }

    /// <summary>
    /// Filter on <see cref="DamageSource.Type"/>
    /// </summary>
    public EnumDamageType? DamageTypeFilter { get; init; }

    public string PoolIdentifier { get; init; }

    public string BaseCode { get; init; }

    public int Length { get; set; }

    public virtual bool IsApplicable(ConnectedClient client, DamageSource src)
    {
        if(ManualAssignmentOnly) return false;
        if(DamageSourceFilter is not null && DamageSourceFilter != src.Source) return false;
        if(DamageTypeFilter is not null && DamageTypeFilter != src.Type) return false;
        if(CausedByEntityFilter is not null && CausedByEntityFilter != (src.CauseEntity is not null)) return false;

        return true;
    }

    public virtual string GetRandomMessage(ConnectedClient client, DamageSource src)
    {
        var code = $"{BaseCode}-{Random.Shared.Next(Length) + 1}";
        var parsedMessage = Lang.GetL(client.Player.LanguageCode, code, client.PlayerName);

        return parsedMessage != code ? parsedMessage : null;
    }
}
