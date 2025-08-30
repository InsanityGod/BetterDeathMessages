using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace BetterDeathMessages.Code.HarmonyPatches.Compatibility;

[HarmonyPatchCategory("hydrateordiedrate")]
[HarmonyPatch("HydrateOrDiedrate.EntityBehaviorThirst", "ApplyThirstDamage")]
public static class HydrateOrDieDrate
{
    public static void Prefix(EntityBehavior __instance, float thirstDamage)
    {
        if(thirstDamage <= 0 || __instance.entity is not EntityPlayer playerEntity) return;
        playerEntity.OverrideDeathMessagePool("dehydrate");
    }
}
