namespace Core.Pawn.Stats
{
    public interface IStatsProvider
    {
        PawnStats GetStats { get; }
    }

    public static class IStatsProviderExtension
    {
        public static float MaxHealth(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.MaxHealth : 0f;

        public static float DefaultSpeed(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.DefaultSpeed : 0f;

        public static float JumpHeight(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.JumpHeight : 0f;

        public static float JumpSpeed(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.JumpSpeed : 0f;

        public static float AttackDamage(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.AttackDamage : 0f;

        public static float AttackSpeed(this IStatsProvider provider) =>
            provider != null ? provider.GetStats.AttackSpeed : 0f;
    }
}