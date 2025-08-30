using BetterDeathMessages.Code.Behaviors;
using BetterDeathMessages.Code.DeathMessageGeneration;
using System;
using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.Server;

namespace BetterDeathMessages.Code;

public partial class DeathMessagePool
{
    public static List<IDeathMessagePool> Pools { get; internal set; } = GenerateDefaultPools();

    private static IDeathMessagePool GetMessagePool(ConnectedClient client, DamageSource src)
    {
        var applicablePools = new List<IDeathMessagePool>();
        foreach (var pool in Pools)
        {
            if(pool.IsApplicable(client, src))
            {
                applicablePools.Add(pool);
            }
        }
        if(applicablePools.Count == 0) return null;

        return applicablePools[Random.Shared.Next(applicablePools.Count - 1)];
    }

    public static string GetMessageFor(ConnectedClient client, DamageSource src)
    {
        if(src == null) return null;
        if(src.Source == EnumDamageSource.Block && src.Type == EnumDamageType.Suffocation) src.Source = EnumDamageSource.Drown; //HACK: Game bug... this should be Drown

        var provider = client.Entityplayer.GetBehavior<DeathMessageProvider>();
        if (provider is not null && !string.IsNullOrEmpty(provider.CustomDeathMessageCode))
        {
            var parsedMessage = Lang.GetL(client.Player.LanguageCode, provider.CustomDeathMessageCode, client.PlayerName);
            if (parsedMessage != provider.CustomDeathMessageCode) return parsedMessage;
        }

        var pool = provider?.CustomDeathMessagePool ?? GetMessagePool(client, src);
        if (pool == null) return null;

        return pool.GetRandomMessage(client, src);
    }

    internal static List<IDeathMessagePool> GenerateDefaultPools() => [
        new DeathMessagePool {
            PoolIdentifier = "drown",
            DamageSourceFilter = EnumDamageSource.Drown,
            BaseCode = "betterdeathmessages:drown",
            Length = 8
        },
        new DeathMessagePool {
            PoolIdentifier = "suicide",
            DamageSourceFilter = EnumDamageSource.Suicide,
            BaseCode = "betterdeathmessages:suicide",
            Length = 4
        },
        new DeathMessagePool {
            PoolIdentifier = "internal-poison",
            DamageSourceFilter = EnumDamageSource.Internal,
            DamageTypeFilter = EnumDamageType.Poison,
            BaseCode = "betterdeathmessages:internal-poison",
            Length = 4
        },
        new DeathMessagePool {
            PoolIdentifier = "weather-frost",
            DamageSourceFilter = EnumDamageSource.Weather,
            DamageTypeFilter = EnumDamageType.Frost,
            BaseCode = "betterdeathmessages:weather-frost",
            Length = 4
        },
        new DeathMessagePool {
            PoolIdentifier = "block-crushing",
            DamageSourceFilter = EnumDamageSource.Block,
            DamageTypeFilter = EnumDamageType.Crushing,
            BaseCode = "betterdeathmessages:block-crushing",
            Length = 6
        },
        new DeathMessagePool {
            PoolIdentifier = "block-piercingattack",
            DamageSourceFilter = EnumDamageSource.Block,
            DamageTypeFilter = EnumDamageType.PiercingAttack,
            BaseCode = "betterdeathmessages:block-piercingattack",
            Length = 4
        },
        new DeathMessagePool {
            PoolIdentifier = "dehydrate",
            ManualAssignmentOnly = true,
            BaseCode = "betterdeathmessages:dehydrate",
            Length = 5
        },
    ];
}
