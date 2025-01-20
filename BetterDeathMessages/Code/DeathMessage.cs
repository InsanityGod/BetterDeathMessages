using BetterDeathMessages.Code.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Util;
using Vintagestory.Server;

namespace BetterDeathMessages.Code
{
    public static class DeathMessage
    {
        public static readonly Dictionary<string, List<string>> MessagePools = new()
        {
            {
                "drown", 
                new()
                {
                    "drown",
                    "fish",
                    "breath",
                    "forgot",
                    "discovered",
                    "shore",
                    "rock",
                }
            },
            {
                "suicide",
                new()
                {
                    "enough",
                    "deleted",
                    "endit",
                    "goodbye",
                }
            },
            {
                "internal-poison",
                new()
                {
                    "bad-choice",
                    "poisoned",
                    "trust",
                    "forgot",
                }
            },
            {
                "weather-frost",
                new()
                {
                    "froze",
                    "icecube",
                    "icesculpture",
                    "underestimated",
                }
            },
            {
                "block-crushing",
                new()
                {
                    "crushed",
                    "outoftheway",
                    "burried",
                    "wrong",
                    "sideways",
                    "forgot",
                }
            },
            {
                "dehydrate",
                new()
                {
                    "dehydrated",
                    "dried",
                    "forgot",
                    "diedrated",
                    "find"
                }
            }

        };

        private static readonly EnumDamageSource[] IgnoreSource = new EnumDamageSource[]
        {
            EnumDamageSource.Explosion,
            EnumDamageSource.Fall,
        };
        
        private static readonly EnumDamageType[] IgnoreType = new EnumDamageType[]
        {
            EnumDamageType.Hunger,
            EnumDamageType.Fire,
            EnumDamageType.Electricity,
        };

        public static List<string> GetPoolFor(DamageSource src, out string poolName)
        {
            var postFix = src.CauseEntity != null ? "-causedby" : string.Empty;
            poolName = $"{src.Source.ToString().ToLower()}-{src.Type.ToString().ToLower()}{postFix}";
            if(MessagePools.TryGetValue(poolName, out var pool) && pool.Count > 0) return pool;
            poolName = $"{src.Source.ToString().ToLower()}{postFix}";
            if(MessagePools.TryGetValue(poolName, out pool) && pool.Count > 0) return pool;
            poolName = $"{src.Type.ToString().ToLower()}{postFix}";
            if(MessagePools.TryGetValue(poolName, out pool) && pool.Count > 0) return pool;
            
            return null;
        }

        public static string GetFor(ConnectedClient client, DamageSource src)
        {
            if(src == null || IgnoreSource.Contains(src.Source) || IgnoreType.Contains(src.Type)) return null; //These are implemented by game already

            if(src.Source == EnumDamageSource.Block && src.Type == EnumDamageType.Suffocation) src.Source = EnumDamageSource.Drown; //Game bug... this should be Drown

            bool useCustom = false;
            var pool = GetPoolFor(src, out string poolName);
            
            var provider = client.Entityplayer.GetBehavior<DeathMessageProvider>();
            if (provider != null && !string.IsNullOrEmpty(provider.CustomDeathMessagePool))
            {
                poolName = provider.CustomDeathMessagePool;
                useCustom = MessagePools.TryGetValue(poolName, out pool);
            }
            string messagecode = null;

            if (pool != null)
            {
                messagecode = $"betterdeathmessages:{poolName}-{pool[Random.Shared.Next(0, pool.Count - 1)]}";
            }

            if (provider != null && !string.IsNullOrEmpty(provider.CustomDeathMessageCode))
            {
                useCustom = true;
                messagecode = provider.CustomDeathMessageCode;
            }

            if(string.IsNullOrEmpty(messagecode)) return null;

            var langParams = new object[] { client.PlayerName };
            
            if(src.CauseEntity != null && !useCustom)
            {
                //TODO append entity info
            }
            
            var parsedMessage = Lang.GetL(client.Player.LanguageCode, messagecode, langParams);
            if(parsedMessage == messagecode) parsedMessage = Lang.GetL("en", messagecode, langParams);
            if(parsedMessage != messagecode) return parsedMessage;
            return null;
        }
    }
}
