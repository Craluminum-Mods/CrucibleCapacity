using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace CrucibleCapacity;

public class Core : ModSystem
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

		api.World.Config.SetInt("CrucibleCapacityPerSlot", CrucibleCapacityConfig.Loaded.CrucibleCapacityPerSlot);
	}

    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (Block block in api.World.Blocks)
        {
            if (block is BlockSmeltingContainer or BlockSmeltedContainer)
			{
				block.Attributes ??= new JsonObject(new JObject());
                block.Attributes.Token["maxContainerSlotStackSize"] = JToken.FromObject(api.World.Config.GetInt("CrucibleCapacityPerSlot"));
            }
        }
    }

	public class CrucibleCapacityConfig : ModSystem
	{
		public static CrucibleCapacityConfig Loaded { get; set; } = new CrucibleCapacityConfig();
		public int CrucibleCapacityPerSlot { get; set; } = 15;
	}
}