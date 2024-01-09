using System;

namespace LevelSystem
{
    public interface ILevelable
    {
        int Level { get; }
        event Action LevelChanged;
        event Action CurrentExperienceChanged;
        int CurrentExperience { get; }
        int RequiredExperience { get; }
        bool IsInitialized { get; }
        event Action Initialized;
        event Action WillUninitialized;
    }
}