public static class Constants
{
    public static readonly string LAYER_DRAGGABLE = "Draggable";
    
    public static readonly string PLAYERDATA_FILENAME = "GameData";
    public static readonly string SAVEFILE_EXTENSION = "NA";
    
}

public enum GameElementType
{
    None = 0,
    BasicTower = 12419,
    BomberTower = 85211,
    BasicEnemy = 83712,
    LinearProjectile = 98125
}
public enum TowerType
{
    BasicTower = GameElementType.BasicTower,
    BomberTower = GameElementType.BomberTower,
}

public enum EnemyType
{
    BasicEnemy = GameElementType.BasicEnemy
}

public enum ProjectileType
{
    LinearProjectile = GameElementType.LinearProjectile
}

public enum TowerStateType
{
    OnDeck,
    OnSelected,
    OnField
}

public enum GameStateType
{
    Starting,
    InGame,
    LevelEnd
}

public enum VFXType
{
    DamageText
}

public enum CurrencyType
{
    Coin,
}

