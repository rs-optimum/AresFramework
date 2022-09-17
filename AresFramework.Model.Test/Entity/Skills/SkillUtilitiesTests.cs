using System;
using System.Reflection;
using AresFramework.Model.Entity.Skills;
using NUnit.Framework;

namespace AresFramework.Model.Test.Entity.Skills;

public class SkillUtilitiesTests
{
    [Test]
    public void GetExperienceForLevel_TestCorrectLevels_ReturnsCorrectExperienceRequired()
    {
        Assert.AreEqual(83, SkillUtilities.GetExperienceForLevel(2));
        Assert.AreEqual(13034431, SkillUtilities.GetExperienceForLevel(99));
        Assert.AreEqual(6517253, SkillUtilities.GetExperienceForLevel(92));
        Assert.AreEqual(101333, SkillUtilities.GetExperienceForLevel(50));
    }
    
    [Test]
    public void GetExperienceForLevel_TestBoundaries_ReturnsNegative1()
    {
        Assert.AreEqual(-1, SkillUtilities.GetExperienceForLevel(0));
        Assert.AreEqual(-1, SkillUtilities.GetExperienceForLevel(0));
        Assert.AreEqual(0, SkillUtilities.GetExperienceForLevel(1));
        Assert.AreEqual(-1, SkillUtilities.GetExperienceForLevel(100));
    }
}
