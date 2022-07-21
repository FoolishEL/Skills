using System;
using UnityEngine;

namespace Core.Pawn.Stats.StatsDecorator
{
    public abstract class AbstractStatsDecorator : IStatsProvider,IDisposable
    {
        protected IStatsProvider wrappedEntity;
        protected PawnStats internalStatsValue;
        private IDisposable _disposableImplementation;

        public event Action OnStatsChanged = delegate
        {
#if UNITY_EDITOR
            Debug.LogError($"Stats was modified!");
#endif
        };

        protected void NotifyStatsChange() => OnStatsChanged.Invoke();

        public AbstractStatsDecorator(IStatsProvider statsProvider)
        {
            wrappedEntity = statsProvider;
            internalStatsValue = wrappedEntity.GetStats.GetCopy();
            if (wrappedEntity is AbstractStatsDecorator decorator)
                decorator.OnStatsChanged += RecalculateInternalValues;
        }

        public void Dispose()
        {
            if (wrappedEntity is AbstractStatsDecorator decorator)
                decorator.OnStatsChanged -= RecalculateInternalValues;
        }

        public PawnStats GetStats => internalStatsValue;

        protected abstract void RecalculateInternalValues();
    }
}