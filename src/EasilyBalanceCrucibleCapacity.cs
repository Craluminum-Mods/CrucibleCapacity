using Vintagestory.API.Common;

namespace EasilyBalanceCrucibleCapacity
{
  public class Configuration : ModSystem
  {
    public override void StartPre(ICoreAPI api)
    {
      base.StartPre(api);

			try
			{
				CrucibleCapacityConfig FromDisk;
				if ((FromDisk = api.LoadModConfig<CrucibleCapacityConfig>("CrucibleCapacityConfig.json")) == null)
				{
					api.StoreModConfig<CrucibleCapacityConfig>(CrucibleCapacityConfig.Loaded, "CrucibleCapacityConfig.json");
				}
				else CrucibleCapacityConfig.Loaded = FromDisk;
			}
			catch
			{
				api.StoreModConfig<CrucibleCapacityConfig>(CrucibleCapacityConfig.Loaded, "CrucibleCapacityConfig.json");
			}

      api.World.Config.SetInt($"ebccCrucibleCapacityPerSlot", CrucibleCapacityConfig.Loaded.CrucibleCapacityPerSlot);
    }

    public class CrucibleCapacityConfig : ModSystem
    {
      public static CrucibleCapacityConfig Loaded { get; set; } = new CrucibleCapacityConfig();
      public int CrucibleCapacityPerSlot { get; set; } = 15;
    }
  }
}