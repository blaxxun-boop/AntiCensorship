using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace AntiCensorship
{
	[BepInPlugin(ModGUID, ModName, ModVersion)]
	public class AntiCensorship : BaseUnityPlugin
	{
		private const string ModName = "Anti Censorship";
		private const string ModVersion = "1.0";
		private const string ModGUID = "org.bepinex.plugins.anticensorship";
		
		public void Awake()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			Harmony harmony = new(ModGUID);
			harmony.PatchAll(assembly);
		}

		[HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.CensorShittyWords))]
		private static class Patch_FejdStartup_CensorShittyWords
		{
			private static bool Prefix(string str, out string __result)
			{
				__result = str;
				return false;
			}
		}
	}
}
