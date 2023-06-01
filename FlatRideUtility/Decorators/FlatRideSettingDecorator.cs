using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public class FlatRideSettingDecorator : IFlatRideDecorator
    {
        public void Decorate<T>(T flatride, GameObject go, Asset asset, AssetBundle assetbundle) where T : FlatRide
        {
            flatride.categoryTag = asset.FlatRideCategory;
            flatride.description = asset.Description;
            flatride.rainProtection = asset.RainProtection;

            flatride.xSize = asset.FootprintX;
            flatride.zSize = asset.FootprintZ;

            flatride.excitementRating = asset.Excitement;
            flatride.intensityRating = asset.Intensity;
            flatride.nauseaRating = asset.Nausea;

            flatride.entranceExitBuilderGO = ScriptableSingleton<AssetManager>.Instance.flatRideEntranceExitBuilderGO;
            flatride.entranceGO = ScriptableSingleton<AssetManager>.Instance.attractionEntranceGO;
            flatride.exitGO = ScriptableSingleton<AssetManager>.Instance.attractionExitGO;
        }
    }
}
