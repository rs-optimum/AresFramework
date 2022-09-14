namespace AresFramework.Model.World;

/// <summary>
/// Represents a position in the game world
/// </summary>
/// <param name="X">The x coordinate</param>
/// <param name="Y">The y coordinate</param>
/// <param name="Z">The z coordinate</param>
public record Position(int X, int Y, int Z = 0)
{
    public int X { get; } = X;
    public int Y { get; } = Y;
    public int Z { get; } = Z;

    public Position ClonePosition()
    {
        return new Position(X, Y, Z);
    }
    
    public Position(Position position)
    {
        X = position.X;
        Y = position.Y;
        Z = position.Z;
    }

    public Position TranslateX(int x)
    {
        return new Position(X + x, Y, Z);
    }

    public Position TranslateY(int y)
    {
        return new Position(X, Y + y, Z);
    }

    public Position Translate(int x, int y)
    {
        return new Position(X + x, Y + y, Z);
    }
    
    public Position Translate(int x, int y, int z)
    {
        return new Position(X + x, Y + y, Z + z);
    }
}