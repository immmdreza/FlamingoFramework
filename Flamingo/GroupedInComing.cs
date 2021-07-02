using Flamingo.Fishes;

namespace Flamingo
{
    /// <summary>
    /// Shows an handler group
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct GroupedInComing<T>
    {
        /// <summary>
        /// Shows an handler group
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public GroupedInComing(IFish<T> inComingFish, int handlingGroup)
        {
            InComingFish = inComingFish;
            HandlingGroup = handlingGroup;
        }

        /// <summary>
        /// The incoming handler itself
        /// </summary>
        public IFish<T> InComingFish { get; }

        /// <summary>
        /// Group rank, lower rank sooner process
        /// </summary>
        public int HandlingGroup { get; }

        /// <summary>
        /// Compare them!
        /// </summary>
        public int CompareTo(GroupedInComing<T> b)
        {
            if (InComingFish.Equals(b.InComingFish))
            {
                return 0;
            }
            else
            {
                int result = HandlingGroup.CompareTo(b.HandlingGroup);

                if (result == 0)
                    return 1;
                return result;
            }
        }
    }
}
