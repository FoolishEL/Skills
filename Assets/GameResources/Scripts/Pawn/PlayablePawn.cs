using Core.Pawn.Stats;

namespace GameResources.Scripts.Pawn
{
    public class PlayablePawn : IStatsProvider
    {
        protected PawnStats currentStats;
        public PawnStats GetStats => currentStats;
    }
}