using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace AntiCensorship;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class AntiCensorship : BaseUnityPlugin
{
	private const string ModName = "Anti Censorship";
	private const string ModVersion = "1.0.2";
	private const string ModGUID = "org.bepinex.plugins.anticensorship";

	public void Awake()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Harmony harmony = new(ModGUID);
		harmony.PatchAll(assembly);
	}

	[HarmonyPatch]
	private static class BypassCensoring
	{
		static IEnumerable<MethodBase> TargetMethods() => typeof(CensorShittyWords).GetMethods().Where(m => m.Name == "Filter");

		private static bool Prefix(string input, out string output, out bool __result)
		{
			output = input;
			__result = false;
			return false;
		}
	}
}
