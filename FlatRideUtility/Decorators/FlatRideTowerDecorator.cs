using FlatRideUtility.Util;
using Parkitect.Mods.AssetPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public class FlatRideTowerDecorator
    {
        public const string TOP = "Top";
        public const string MIDDLE = "Middle";

        public const string TOP_FIELD = "topSectionGO";
        public const string MID_FIELD = "midSectionsGOs";
        public const string BOUND_FIELD = "towerBoundingBox";

        public void Decorate<T>(T flatRide, GameObject go, AssetPack assetpack, AssetBundle assetbundle) where T : FlatRide
        {
            // Get the tower ride assets
            Asset flatRideAsset = AssetUtility.GetAsset(assetpack, FlatRideGlobals.FLATRIDE);
            if (flatRideAsset == null)
            {
                FileIO.Write($"Could not find asset with name {FlatRideGlobals.FLATRIDE}");
            }

            Asset topAsset = AssetUtility.GetAsset(assetpack, TOP);
            if (topAsset == null)
            {
                FileIO.Write($"Could not find asset with name {TOP}");
            }

            Asset[] middleAssets = AssetUtility.GetOrderAssets(assetpack, MIDDLE);
            if (middleAssets.Length == 0 || middleAssets == null)
            {
                FileIO.Write($"Could not find assets with name {MIDDLE}");
            }

            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField | BindingFlags.SetField;

            // Set tower bounding box
            Parkitect.Mods.AssetPacks.BoundingBox oldBoundingBox = flatRideAsset.BoundingBoxes[0];
            BoundingBox newBoundingBox = go.AddComponent<BoundingBox>();
            Bounds newBounds = default(Bounds);
            Vector3 min = new Vector3(oldBoundingBox.BoundsMin[0], oldBoundingBox.BoundsMin[1], oldBoundingBox.BoundsMin[2]);
            Vector3 max = new Vector3(oldBoundingBox.BoundsMax[0], oldBoundingBox.BoundsMax[1], oldBoundingBox.BoundsMax[2]);
            newBounds.SetMinMax(min, max);
            newBoundingBox.setBounds(newBounds);
            newBoundingBox.layers = BoundingVolume.Layers.Buildvolume;

            FieldInfo boundingField = typeof(TowerRide).GetField(BOUND_FIELD, bindingFlags);
            boundingField.SetValue(flatRide, newBoundingBox);

            // Using reflection to set the GOs as theyre locked away
            FieldInfo topSectionField = typeof(TowerRide).GetField(TOP_FIELD, bindingFlags);
            FieldInfo midSectionField = typeof(TowerRide).GetField(MID_FIELD, bindingFlags);

            GameObject topGO = assetbundle.LoadAsset<GameObject>(topAsset.Guid);
            new MaterialDecorator().Decorate(topGO, flatRideAsset, assetbundle);
            topSectionField.SetValue(flatRide, topGO);

            GameObject[] midGOs = new GameObject[middleAssets.Length];
            for (int l = 0; l < midGOs.Length; l++)
            {
                midGOs[l] = assetbundle.LoadAsset<GameObject>(middleAssets[l].Guid);
                new MaterialDecorator().Decorate(midGOs[l], flatRideAsset, assetbundle);
            }
            midSectionField.SetValue(flatRide, midGOs);
        }
    }
}
