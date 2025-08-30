using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.Server;

namespace BetterDeathMessages.Code.HarmonyPatches;

[HarmonyPatch(typeof(ServerSystemEntitySimulation), "GetDeathMessage")]
public static class GetDeathMessageOverride
{
    public static bool Prefix(ConnectedClient client, DamageSource src, ref string __result)
    {
        var custom = DeathMessagePool.GetMessageFor(client, src);
        if (string.IsNullOrEmpty(custom)) return true;

        __result = custom;
        return false;
    }
}
