using System;
using UnityEngine;

namespace Core.Pawn.Stats
{
    [Serializable]
    public sealed class PawnStats
    {
        [SerializeField] private float maxHealth;
        public float MaxHealth => maxHealth;

        [SerializeField] private float defaultSpeed;
        public float DefaultSpeed => defaultSpeed;

        [SerializeField] private float jumpHeight;
        public float JumpHeight => jumpHeight;

        [SerializeField] private float jumpSpeed;
        public float JumpSpeed => jumpSpeed;

        [SerializeField] private float attackDamage;
        public float AttackDamage => attackDamage;

        [SerializeField] private float attackSpeed;
        public float AttackSpeed => attackSpeed;

        /// <summary>
        /// Get memberwise clone.
        /// </summary>
        public PawnStats GetCopy() => (PawnStats)this.MemberwiseClone();

        public PawnStats(float maxHealth, float defaultSpeed,float jumpHeight,float jumpSpeed,float attackDamage,float attackSpeed)
        {
            this.maxHealth = maxHealth;
            this.defaultSpeed = defaultSpeed;
            this.jumpHeight = jumpHeight;
            this.jumpSpeed = jumpSpeed;
            this.attackDamage = attackDamage;
            this.attackSpeed = attackSpeed;
        }

        public void Deconstruct(out float maxHealth,out float defaultSpeed, out float jumpHeight, out float jumpSpeed, out float attackDamage, out float attackSpeed)
        {
            maxHealth = MaxHealth;
            defaultSpeed = DefaultSpeed;
            jumpHeight = JumpHeight;
            jumpSpeed = JumpSpeed;
            attackDamage = AttackDamage;
            attackSpeed = AttackSpeed;
        }
    }

    public enum PawnStatsType
    {
        MaxHealth,
        DefaultSpeed,
        JumpHeight,
        JumpSpeed,
        AttackDamage,
        AttackSpeed
    }

    public static class PawnStatsExtension
    {
        public static float GetStatByType(this PawnStats pawnStats, PawnStatsType pawnStatsType)
        {
            if (pawnStats != null)
                switch (pawnStatsType)
                {
                    case PawnStatsType.MaxHealth:
                        return pawnStats.MaxHealth;

                    case PawnStatsType.DefaultSpeed:
                        return pawnStats.DefaultSpeed;

                    case PawnStatsType.JumpHeight:
                        return pawnStats.JumpHeight;

                    case PawnStatsType.JumpSpeed:
                        return pawnStats.JumpSpeed;

                    case PawnStatsType.AttackDamage:
                        return pawnStats.AttackDamage;

                    case PawnStatsType.AttackSpeed:
                        return pawnStats.AttackSpeed;
                }
            return 0f;
        }
    }
}