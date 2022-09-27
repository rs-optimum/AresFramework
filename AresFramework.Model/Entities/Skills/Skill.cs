namespace AresFramework.Model.Entities.Skills;

public struct Skill
{
    public readonly int CurrentLevel;

    public readonly double Experience;

    public readonly int MaximumLevel;

    public Skill(int currentLevel, double experience)
    {
        CurrentLevel = currentLevel;
        Experience = experience;
        MaximumLevel = SkillUtilities.GetLevelForExperience(experience);
    }
    
    
    public const int Attack = 0;
    public const int Defence = 1;
    public const int Strength = 2;
    public const int Hitpoints = 3;
    public const int Ranged = 4;
    public const int Prayer = 5;
    public const int Magic = 6;
    public const int Cooking = 7;
    public const int Woodcutting = 8;
    public const int Fletching = 9;
    public const int Fishing = 10;
    public const int Firemaking = 11;
    public const int Crafting = 12;
    public const int Smithing = 13;
    public const int Mining = 14;
    public const int Herblore = 15;
    public const int Agility = 16;
    public const int Thieving = 17;
    public const int Slayer = 18;
    public const int Farming = 19;
    public const int Runecrafting = 20;
    public const int Hunter = 21;
    public const int Construction = 22;
}