using System.Diagnostics.Contracts;

namespace AresFramework.Model.Entities.Skills;

public static class SkillUtilities
{   
    public const double MaximumExperience = 200_000_000;
    private static readonly int[] ExperienceForLevel = new int[100];
    private static readonly int SkillsCount = 20;
    
    
    
    static SkillUtilities()
    {
        int points = 0;
        int output = 0;
        for (int level = 1; level <= 99; level++) {
            ExperienceForLevel[level] = output;
            points += (int) Math.Floor(level + 300 * Math.Pow(2, (double) level / 7.0));
            output = (int) Math.Floor((decimal) (points / 4));
        }
    }
    
    /// <summary>
    /// Gets the experience required for a specific level
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static int GetExperienceForLevel(int level)
    {
        if (level is <= 0 or > 99)
        {
            return -1;
        }
        return ExperienceForLevel[level];
    }
    
    /// <summary>
    /// Gets the level for the experience you have
    /// </summary>
    /// <param name="experience">the amount of experience</param>
    /// <returns></returns>
    public static int GetLevelForExperience(double experience)
    {
        for (int level = 1; level <= 98; level++) {
            if (experience < ExperienceForLevel[level + 1]) {
                return level;
            }
        }
        return 99;
    }
}