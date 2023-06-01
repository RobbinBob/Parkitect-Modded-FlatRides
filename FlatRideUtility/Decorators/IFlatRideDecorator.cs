using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public interface IFlatRideDecorator
    {
        void Decorate<T>(T flatride, GameObject go, Asset asset, AssetBundle assetbundle) where T : FlatRide;
    }
}
