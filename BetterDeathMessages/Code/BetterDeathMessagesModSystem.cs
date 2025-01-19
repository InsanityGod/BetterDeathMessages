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
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            if (!Harmony.HasAnyPatches(Mod.Info.ModID))
            {
                harmony = new Harmony(Mod.Info.ModID);
                harmony.PatchAllUncategorized();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            harmony?.UnpatchAll();
        }
    }
}
