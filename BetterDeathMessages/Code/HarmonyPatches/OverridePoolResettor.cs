using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace BetterDeathMessages.Code.HarmonyPatches
{
    [HarmonyPatch(typeof(Entity), nameof(Entity.ReceiveDamage))]
    public static class OverridePoolResettor
    {
        public static void Postfix(Entity __instance, ref bool __result)
        {
            if(__instance is not EntityPlayer player) return;
        }
    }
}
