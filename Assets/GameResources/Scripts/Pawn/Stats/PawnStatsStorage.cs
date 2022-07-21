using UnityEngine;

namespace Core.Pawn.Stats
{
    [CreateAssetMenu(menuName = "Core/Pawn/Stats")]
    public class PawnStatsStorage : ScriptableObject, IStatsProvider
    {
        [SerializeField] private PawnStats pawnStats;
        public PawnStats GetStats => pawnStats;
    }
}