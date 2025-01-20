using BetterDeathMessages.Code.Behaviors;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace BetterDeathMessages.Code
{
    public class BetterDeathMessagesModSystem : ModSystem
    {
        //public Dict
        private Harmony harmony;
        // Called on server and client
        // Useful for registering block/entity classes on both sides

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);

            api.RegisterEntityBehaviorClass($"BetterDeathMessages.DeathMessageProvider", typeof(DeathMessageProvider));

            if (!Harmony.HasAnyPatches(Mod.Info.ModID))
            {
                harmony = new Harmony(Mod.Info.ModID);
                harmony.PatchAllUncategorized();

                TryAddCompatibility(api, harmony, "hydrateordiedrate");
            }
        }

        public void TryAddCompatibility(ICoreAPI api, Harmony harmony, string modId)
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
        }
    }
}
