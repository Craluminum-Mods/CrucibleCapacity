using Vintagestory.API.Common;

namespace CrucibleCapacity
{
  public class Configuration : ModSystem
  {
    public override void StartPre(ICoreAPI api)
    {
		try
		{
			CrucibleCapacityConfig FromDisk;
			if ((FromDisk = api.LoadModConfig<CrucibleCapacityConfig>("CrucibleCapacityConfig.json")) == null)
			{
				api.StoreModConfig(CrucibleCapacityConfig.Loaded, "CrucibleCapacityConfig.json");
			}
			else CrucibleCapacityConfig.Loaded = FromDisk;
		}
		catch
		{
			api.StoreModConfig(CrucibleCapacityConfig.Loaded, "CrucibleCapacityConfig.json");
		}

      api.World.Config.SetInt($"CrucibleCapacityPerSlot", CrucibleCapacityConfig.Loaded.CrucibleCapacityPerSlot);
    }

    public class CrucibleCapacityConfig : ModSystem
    {
      public static CrucibleCapacityConfig Loaded { get; set; } = new CrucibleCapacityConfig();
      public int CrucibleCapacityPerSlot { get; set; } = 15;
    }
  }
}