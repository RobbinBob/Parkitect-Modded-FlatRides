using FlatRideUtility.Util;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public class FlatRideSeatDecorator : IFlatRideDecorator
    {
        public void Decorate<T>(T flatRide, GameObject go, Asset asset, AssetBundle assetbundle) where T : FlatRide
        {
            if (flatRide.seatConfigurations == null || flatRide.seatConfigurations.Length == 0)
            {
                FileIO.WriteException($"{flatRide.name} failed to find seats");
                throw new System.NullReferenceException();
            }
            AttractionSeatConfiguration[] seatConfigurations = flatRide.seatConfigurations;
            for (int seatIndex = 0; seatIndex < seatConfigurations.Length; seatIndex++)
            {
                // You can assign seats to different sitting types when directly accessing the AttractionSeatConfiguration object
                seatConfigurations[seatIndex].sittingType = asset.SittingType;
            }
        }
    }
}
