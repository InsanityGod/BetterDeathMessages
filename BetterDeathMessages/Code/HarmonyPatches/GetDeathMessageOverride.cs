using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Util;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.HarmonyPatches
{
    [HarmonyPatch(typeof(ServerSystemEntitySimulation), "GetDeathMessage")]
    public static class GetDeathMessageOverride
    {
        public static bool Prefix(ConnectedClient client, DamageSource src, ref string __result)
        {
            var custom = DeathMessage.GetFor(client, src);
            if (string.IsNullOrEmpty(custom)) return true;

            __result = custom;
            return false;
        }
    }
}
