using FlatRideUtility.Decorators;
using FlatRideUtility.Util;
using Parkitect.Mods.AssetPacks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FlatRideUtility
{
    /// <summary>
    /// A class used for creating <see cref="FlatRide"/>'s at runtime
    /// </summary>
    public class FlatRideBinder
    {
        private readonly List<Object> registeredObjects = new List<Object>();
        private readonly string binderHash;

        private bool m_IsApplied = false;

        /// <summary>
        /// Create new <see cref="FlatRideBinder"/> with hash <paramref name="hash"/> for modifying FlatRides
        /// </summary>
        /// <param name="hash"></param>
        public FlatRideBinder(string hash)
        {
            binderHash = hash;
        }

        private string GetNameHash(string name)
        {
            return string.Concat(name, "-", binderHash);
        }


        /// <summary>
        /// Returns the <see cref="FlatRideBinder"/> patch state.
        /// </summary>
        /// <returns><see langword="true"/> if applied, otherwise <see langword="false"/>.</returns>
        public bool IsApplied() => m_IsApplied;


        /// <summary>
        /// Adds all data from this binder into the games asset list
        /// </summary>
        public void Apply()
        {
            try
            {
                for (int i = 0; i < registeredObjects.Count; i++)
                {
                    FileIO.Write($"Binding FlatRide of Type: {registeredObjects[i].GetType().FullName} with Name: {registeredObjects[i].name}");
                    ScriptableSingleton<AssetManager>.Instance.registerObject(registeredObjects[i]);
                }
                m_IsApplied = true;
            }
            catch (Exception e)
            {
                FileIO.WriteException("Failed to apply all FlatRides");
                FileIO.WriteException(e.ToString());
            }
        }
        /// <summary>
        /// Removes the data this binder has from the game asset list
        /// </summary>
        public void Remove()
        {
            try
            {
                for (int i = 0; i < registeredObjects.Count; i++)
                {
                    ScriptableSingleton<AssetManager>.Instance.unregisterObject(registeredObjects[i]);
                }
                m_IsApplied = false;
            }
            catch (Exception e)
            {
                FileIO.WriteException("Failed to remove all FlatRides");
                FileIO.WriteException(e.ToString());
            }
        }


        /// <summary>
        /// Registers a duplicate of a <see cref="FlatRide"/> of type <typeparamref name="T"/>, with new name
        /// </summary>
        /// <typeparam name="T">The type of flatride to duplicate</typeparam>
        /// <param name="unlocalizedName">The original flatride name that will be duplicated</param>
        /// <param name="name">The new flatride name that will be used as its identification</param>
        /// <param name="displayname">The new name the flatride will display</param>
        /// <returns>The <see cref="FlatRide"/> component</returns>
        public T RegisterDuplicateFlatRide<T>(string unlocalizedName, string name, string displayname)
            where T : FlatRide
        {
            T flatRide = (T)Object.Instantiate(FlatRideHelper.GetFlatRide(unlocalizedName));
            flatRide.name = GetNameHash(name);
            flatRide.setDisplayName(displayname);
            registeredObjects.Add(flatRide);
            return flatRide;
        }
        /// <summary>
        /// Create a new <see cref="FlatRide"/> of type <typeparamref name="T"/>, attached to an empty <see cref="GameObject"/>
        /// </summary>
        /// <typeparam name="T">The type of flatride to create</typeparam>
        /// <param name="name">The name used as identification for the flatride</param>
        /// <param name="displayname">The name that will be displayed</param>
        /// <returns>The <see cref="FlatRide"/> component</returns>
        public T RegisterNewFlatRide<T>(string name, string displayname) where T : FlatRide
        {
            GameObject go = new GameObject(name);
            T flatRide = go.AddComponent<T>();
            flatRide.name = GetNameHash(name);
            flatRide.setDisplayName(displayname);

            registeredObjects.Add(flatRide);
            return flatRide;
        }
        /// <summary>
        /// Creates a new <see cref="FlatRide"/> of type <typeparamref name="T"/>, setup using a PAE <see cref="AssetPack"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="assetpack"></param>
        /// <param name="assetbundle"></param>
        /// <returns>The <see cref="FlatRide"/> component</returns>
        public T RegisterNewFlatRide<T>(string name, GameObject go) where T : FlatRide
        {
            // The returned component
            T flatRideComponent = go.AddComponent<T>();
            flatRideComponent.name = GetNameHash(name);
            flatRideComponent.setDisplayName(name);

            // Add it to list to be assigned
            registeredObjects.Add(flatRideComponent);
            return flatRideComponent;
        }
    }
}
