namespace AresFramework.Model.Entity.Skills;

public class SkillSet
{
    private readonly Skill[] _skills = new Skill[23];

    public SkillSet(Skill[] skills)
    {
        _skills = skills;
    }
    
    public static SkillSet DefaultSkillSet()
    {
        var skills = new Skill[23];
        skills[Skill.Attack] = new Skill(1, 0);
        skills[Skill.Strength] = new Skill(1, 0);
        skills[Skill.Defence] = new Skill(1, 0);
        skills[Skill.Hitpoints] = new Skill(10, 1154);
        skills[Skill.Ranged] = new Skill(1, 0);
        skills[Skill.Prayer] = new Skill(1, 0);
        skills[Skill.Magic] = new Skill(1, 0);
        skills[Skill.Cooking] = new Skill(1, 0);
        skills[Skill.Woodcutting] = new Skill(1, 0);
        skills[Skill.Fletching] = new Skill(1, 0);
        skills[Skill.Fishing] = new Skill(1, 0);
        skills[Skill.Firemaking] = new Skill(1, 0);
        skills[Skill.Crafting] = new Skill(1, 0);
        skills[Skill.Smithing] = new Skill(1, 0);
        skills[Skill.Mining] = new Skill(1, 0);
        skills[Skill.Herblore] = new Skill(1, 0);
        skills[Skill.Agility] = new Skill(1, 0);
        skills[Skill.Thieving] = new Skill(1, 0);
        skills[Skill.Slayer] = new Skill(1, 0);
        skills[Skill.Farming] = new Skill(1, 0);
        skills[Skill.Runecrafting] = new Skill(1, 0);
        skills[Skill.Hunter] = new Skill(1, 0);
        skills[Skill.Construction] = new Skill(1, 0);
        return new SkillSet(skills);
    }
    
    public Skill GetSkill(int skill)
    {
        return _skills[skill];
    }
    
    public int GetCurrentLevel(int skill)
    {
        return _skills[skill].CurrentLevel;
    }
    
    public void SetLevel(int skill, int level)
    {
        Skill currentSkill = _skills[skill];
        _skills[skill] = new Skill(level, currentSkill.Experience);
    }
    
    public void AddExperience(int skill, double amount)
    {
        Skill currentSkill = _skills[skill];
        _skills[skill] = new Skill(currentSkill.CurrentLevel, currentSkill.Experience + amount);
        
    }

}