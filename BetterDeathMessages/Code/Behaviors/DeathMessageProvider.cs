using BetterDeathMessages.Code.DeathMessageGeneration;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace BetterDeathMessages.Code.Behaviors;

public class DeathMessageProvider(Entity entity) : EntityBehavior(entity)
{
    public override string PropertyName() => nameof(DeathMessageProvider);


    private int IgnoreDamageCount = -1;

    private IDeathMessagePool customDeathMessagePool;
    
    /// <summary>
    /// When assigned overrides which pool will be selected
    /// Is automatically unnasigned after entity has received damage
    /// </summary>
    public IDeathMessagePool CustomDeathMessagePool 
    { 
        get => customDeathMessagePool; 
        set
        {
            customDeathMessagePool = value;
            if (value is not null)
            {
                IgnoreDamageCount = 1;
                customMessageCode = null;
            }
            else if(string.IsNullOrEmpty(customMessageCode))
            {
                IgnoreDamageCount = -1;
            }
        } 
    }

    private string customMessageCode;
    
    /// <summary>
    /// When assigned overrides the language string code used
    /// Is automatically unnasigned after entity has received damage
    /// </summary>
    public string CustomDeathMessageCode
    {
        get => customMessageCode;
        set 
        {
            customMessageCode = value;
            if (!string.IsNullOrEmpty(value))
            {
                IgnoreDamageCount = 1;
                customDeathMessagePool = null;
            }
            else if(customDeathMessagePool is null)
            {
                IgnoreDamageCount = -1;
            }
        }
    }

    public override void OnEntityReceiveDamage(DamageSource damageSource, ref float damage)
    {
        if(IgnoreDamageCount == -1) return;
        IgnoreDamageCount--;

        if(IgnoreDamageCount == -1)
        {
            customDeathMessagePool = null;
            customMessageCode = null;
        }
    }
}
