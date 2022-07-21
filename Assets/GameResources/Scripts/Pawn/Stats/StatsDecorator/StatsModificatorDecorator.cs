using System;
using System.Collections.Generic;
using System.Threading;

namespace Core.Pawn.Stats.StatsDecorator
{
    using StatsModificators;

    public sealed class StatsModificatorDecorator : AbstractStatsDecorator
    {
        private List<StatModificatorWithSource> statsWithSourceModificators;
        public readonly object ModificatorLock = new object();

        public StatsModificatorDecorator(IStatsProvider statsProvider) : base(statsProvider)
        {
            statsWithSourceModificators = new List<StatModificatorWithSource>();
        }

        public void ApplyModificator(StatsModificator statsModificator,IStatModificationSourceProvider statModificationSourceProvider)
        {
            try
            {
                Monitor.Enter(ModificatorLock);
                var statModificatorWithSource = new StatModificatorWithSource
                    { StatsModificator = statsModificator, SourceProvider = statModificationSourceProvider };
                statModificatorWithSource.StartSourceExpirationListening(OnSourceExpired);
                statsWithSourceModificators.Add(statModificatorWithSource);
                RecalculateInternalValues();
            }
            finally
            {
                Monitor.Exit(ModificatorLock);
            }
        }

        private void OnSourceExpired(StatModificatorWithSource source)
        {
            try
            {
                Monitor.Enter(ModificatorLock);
                statsWithSourceModificators.Remove(source);
                RecalculateInternalValues();
            }
            finally
            {
                Monitor.Exit(ModificatorLock);
            }
        }

        protected override void RecalculateInternalValues()
        {
            (var maxHealth, var defaultSpeed,
                    var jumpHeight, var jumpSpeed,
                    var attackDamage, var attackSpeed) =
                wrappedEntity.GetStats;
            internalStatsValue = new PawnStats
            (maxHealth * GetTotalModificator(PawnStatsType.MaxHealth),
                defaultSpeed * GetTotalModificator(PawnStatsType.DefaultSpeed),
                jumpHeight * GetTotalModificator(PawnStatsType.JumpHeight),
                jumpSpeed * GetTotalModificator(PawnStatsType.JumpSpeed),
                attackDamage * GetTotalModificator(PawnStatsType.AttackDamage),
                attackSpeed * GetTotalModificator(PawnStatsType.AttackSpeed)
            );
            NotifyStatsChange();
        }

        ~StatsModificatorDecorator() => statsWithSourceModificators.ForEach(c => c.Dispose());

        private float GetTotalModificator(PawnStatsType statsType)
        {
            float result = 1f;
            foreach (var item in statsWithSourceModificators)
                if (item.StatsModificator.StatsType == statsType)
                    result += item.StatsModificator.PercentageChange;
            return result;
        }
        
        private class StatModificatorWithSource : IDisposable
        {
            public StatsModificator StatsModificator;
            public IStatModificationSourceProvider SourceProvider;
            private Action<StatModificatorWithSource> _actionOnExpires;
            
            public void StartSourceExpirationListening(Action<StatModificatorWithSource> actionOnExpires)
            {
                _actionOnExpires = actionOnExpires;
                SourceProvider.OnSourceExpired += OnExpires;
            }
            private void OnExpires()
            {
                _actionOnExpires.Invoke(this);
                Dispose();
            }
            public void Dispose()
            {
                if (SourceProvider != null)
                    SourceProvider.OnSourceExpired -= OnExpires;
            }
        }
    }
}