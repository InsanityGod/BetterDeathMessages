using BetterDeathMessages.Code.Behaviors;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace BetterDeathMessages.Code;

public class BetterDeathMessagesModSystem : ModSystem
{
    private Harmony harmony;

    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server; //TODO see if this mod is even needed client side and if not then fix modinfo.json

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);

        api.RegisterEntityBehaviorClass("BetterDeathMessages.DeathMessageProvider", typeof(DeathMessageProvider));

        if (!Harmony.HasAnyPatches(Mod.Info.ModID))
        {
            harmony = new Harmony(Mod.Info.ModID);
            harmony.PatchAllUncategorized();

            TryAddCompatibility(api, harmony, "hydrateordiedrate");
        }
    }

    public static void TryAddCompatibility(ICoreAPI api, Harmony harmony, string modId)
    {
        if (api.ModLoader.IsModEnabled(modId))
        {
            try
            {
                harmony.PatchCategory(modId);
            }
            catch
            {
                api.Logger.Error($"Failed to bind BetterDeathMesage compatibility for {modId}");
            }
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        harmony?.UnpatchAll();

        DeathMessagePool.Pools = DeathMessagePool.GenerateDefaultPools();
    }
}
