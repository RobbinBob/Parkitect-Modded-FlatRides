using System;
using FlatRideUtility.Util;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace FlatRideUtility.Decorators
{
    public class FlatRideWaypointDecorator : IFlatRideDecorator
    {
        public void Decorate<T>(T flatRide, GameObject go, Asset asset, AssetBundle assetbundle) where T : FlatRide
        {
            if (asset.Waypoints == null || asset.Waypoints.Count == 0)
            {
                FileIO.WriteException($"Failed to find waypoints");
                throw new NullReferenceException();
            }
            Waypoints newWaypoints = go.GetComponent<Waypoints>();
            foreach (Parkitect.Mods.AssetPacks.Waypoint oldWaypoint in asset.Waypoints)
            {
                Waypoint newWaypoint = new Waypoint()
                {
                    isOuter = oldWaypoint.IsOuter,
                    isRabbitHoleGoal = oldWaypoint.IsRabbitHoleGoal,
                    localPosition = new Vector3(oldWaypoint.Position[0], oldWaypoint.Position[1], oldWaypoint.Position[2]),
                    connectedTo = oldWaypoint.ConnectedTo
                };
                newWaypoints.waypoints.Add(newWaypoint);
            }
        }
    }
}
