using System;

namespace Core.Pawn.Stats.StatsModificators
{
    public interface IStatModificationSourceProvider
    {
        event Action OnSourceExpired;
    }
}