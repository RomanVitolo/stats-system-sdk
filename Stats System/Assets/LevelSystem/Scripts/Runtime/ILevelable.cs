using System;

namespace LevelSystem
{
    public interface ILevelable
    {
        int Level { get; }
        event Action LevelChanged;
        event Action CurrentExperienceChanged;
        int CurrentExperience { get; set; }
        int RequiredExperience { get; }
        bool IsInitialized { get; }
        event Action Initialized;
        event Action WillUninitialized;
    }
}