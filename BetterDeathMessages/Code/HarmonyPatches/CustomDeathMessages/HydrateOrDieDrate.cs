using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace BetterDeathMessages.Code.HarmonyPatches.CustomDeathMessages
{
    [HarmonyPatchCategory("hydrateordiedrate")]
    [HarmonyPatch("EntityBehaviorThirst", "ApplyDamage")]
    public static class HydrateOrDieDrate
    {
        public static void Prefix(EntityBehavior __instance) => (__instance.entity as EntityPlayer)?.OverrideDeathMessagePool("dehydrate");
    }
}
