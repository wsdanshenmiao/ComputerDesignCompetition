public enum SceneType
{
    Location, // 地点场景
    Menu, // 菜单场景
    Special // 有特殊功能的场景
}
public enum FoodType
{
    Die,
    JumpBoost_I,    // A: 跳跃1.5
    JumpBoost_II,   // AA: 跳跃2.0
    JumpBoost_III,  // AAA: 跳跃2.5
    DashBoost_I,    // B: 冲刺3.0
    DashBoost_II,   // BB: 冲刺4.0
    DashBoost_III,  // BBB: 冲刺4.5
    HealthBoost_I,  // C: 生命+1
    HealthBoost_II, // CC: 生命+2
    HealthBoost_III,// CCC: 生命+3
    Clone,          // AB: 分身
    Special_AAB,    // AAB: 食物5 获得变大魔法
    Special_ABB,    // ABB: 食物6 获得变小魔法
    Special_AC,     // AC: 食物7 获得火球术魔法
    Special_BC,     // BC: 食物8 魔力上限提升
    Special_AAC,    // AAC: 食物9 魔力恢复速度提升
    Special_BBC,    // BBC: 食物10 吸铁石
    Special_ABC     // ABC: 食物11 大冲
}

public enum IngredientType
{
    None,   // 空
    A,      // 食材A
    B,      // 食材B
    C       // 食材C
}

public enum SkillType
{
    FireBall,
    Magnet,
    SizeChangeGrow,
    SizeChangeShrink,
    Clone,
    Dash,
} 

public enum SavePriority
{
    CoreSystem = 0,      // 核心系统，最高优先级
    GameState = 100,     // 游戏状态管理器
    MenuManager = 150,   // 菜单管理器
    Inventory = 200,     // 物品栏系统
    BackPack = 250,      // 背包系统
    PlayerData = 300,    // 玩家数据
    Environment = 400,   // 环境相关数据
    Effects = 450,       // 效果系统（如 HealthBoostEffect, DieEffect 等）
    Other = 500         // 其他系统，最低优先级
}
