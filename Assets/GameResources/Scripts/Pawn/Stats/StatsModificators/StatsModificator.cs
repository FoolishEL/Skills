using UnityEngine;

namespace Core.Pawn.Stats.StatsModificators
{
    public class StatsModificator
    {
        public StatsModificator(PawnStatsType pawnStatsType, float percentageChange)
        {
            PercentageChange = Mathf.Abs(percentageChange);
            StatsType = pawnStatsType;
        }
        
        public PawnStatsType StatsType { get; protected set; }
        public float PercentageChange { get; protected set; }

        public float GetNewStatValue(IStatsProvider provider) =>
            provider.GetStats.GetStatByType(StatsType) * PercentageChange;
    }
}