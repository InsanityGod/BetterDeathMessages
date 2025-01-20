using BetterDeathMessages.Code.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;

namespace BetterDeathMessages.Code
{
    public static class Extensions
    {
        public static void OverrideDeathMessagePool(this EntityPlayer entityPlayer, string pool)
        {
            var provider = entityPlayer.GetBehavior<DeathMessageProvider>();
            if(provider == null)
            {
                entityPlayer.Api?.Logger.Warning("Player does not have a DeathMessageProvider assigned? (BetterDeathMessages)");
                return;
            }
            provider.CustomDeathMessagePool = pool;
        }

        public static void OverrideDeathMessageCode(this EntityPlayer entityPlayer, string code)
        {
            var provider = entityPlayer.GetBehavior<DeathMessageProvider>();
            if(provider == null)
            {
                entityPlayer.Api?.Logger.Warning("Player does not have a DeathMessageProvider assigned? (BetterDeathMessages)");
                return;
            }
            provider.CustomDeathMessageCode = code;
        }
    }
}
