namespace FlatRideUtility
{
    /// <summary>
    /// Utility class to help with creating custom <see cref="FlatRide"/>'s, commonly used along with <see cref="FlatRideBinder"/>
    /// </summary>
    public static class FlatRideHelper
    {
        /// <summary>
        /// Attempts to find the <seealso cref="FlatRide"/> with <paramref name="name"/> in Parkitect
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FlatRide GetFlatRide(string name)
        {
            FlatRide flatRide = null;
            foreach (Attraction current in ScriptableSingleton<AssetManager>.Instance.getAttractionObjects())
            {
                if (current.getUnlocalizedName() != name) continue;

                flatRide = (FlatRide)current;
                return flatRide;
            }

            return flatRide;
        }
    }
}
