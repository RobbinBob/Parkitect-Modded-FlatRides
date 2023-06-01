using FlatRideUtility.Util;
using Parkitect.Mods.AssetPacks;
using System.Collections.Generic;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public class FlatRideRestraintDecorator : IFlatRideDecorator
    {
        public Vector3 openAngle = new Vector3();
        public Vector3 closeAngle = new Vector3();
        public bool isClosedByDefault = true;
        public string restraintName = "restraint";

        public void Decorate<T>(T flatride, GameObject go, Asset asset, AssetBundle assetbundle) where T : FlatRide
        {
            List<Transform> transforms = new List<Transform>();
            Utility.recursiveFindTransformsStartingWith(restraintName, go.transform, transforms);

            if (transforms.Count == 0)
            {
                FileIO.WriteException($"No transforms found in children with name {restraintName}");
                return;
            }

            foreach (Transform transform in transforms)
            {
                GameObject childGo = transform.gameObject;
                RestraintRotationController rotationController = childGo.AddComponent<RestraintRotationController>();
                rotationController.startClosed = isClosedByDefault;
                rotationController.openAngles = openAngle;
                rotationController.closedAngles = closeAngle;
            }
        }
    }
}
