using System.Reflection;
using HarmonyLib;

namespace Loafy
{
    public class HarmonyPatches
    {
        private static Harmony Instance;
        private static bool Patched;

        internal static void ApplyPatches()
        {
            if (Patched) 
                return;

            Patched = true;
            Instance = new Harmony(PluginInfo.GUID);
            Instance.PatchAll(Assembly.GetExecutingAssembly());
        }

        internal static void RemovePatches()
        {
            if (!Patched)
                return;

            Patched = false;
            Instance.UnpatchSelf();
            Instance = null;
        }
    }
}
