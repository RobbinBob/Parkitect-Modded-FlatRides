using System.Collections.Generic;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace FlatRideUtility.Util
{
    public static class AssetUtility
    {
        public static Asset GetAsset(AssetPack assetpack, string name)
        {
            List<Asset> assets = assetpack.Assets;
            foreach (Asset asset in assets)
            {
                if (asset.Name == name)
                {
                    return asset;
                }
            }
            return null;
        }
        public static Asset[] GetOrderAssets(AssetPack assetpack, string name)
        {
            List<Asset> assets = new List<Asset>();
            int count = 0;
            while (GetAsset(assetpack, name + '_' + count) != null)
            {
                assets.Add(GetAsset(assetpack, name + '_' + count));
                count++;
            }
            return assets.ToArray();
        }
        public static T LoadFromAssetPack<T>(AssetPack assetpack, AssetBundle assetbundle, string name) where T : Object
        {
            Asset asset = GetAsset(assetpack, name);
            if (asset == null)
            {
                FileIO.WriteException($"Could not find asset with name {name}");
                return null;
            }

            T go = assetbundle.LoadAsset<T>(asset.Guid);
            return go;
        }
    }
}
