using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace BetterDeathMessages.Code.Behaviors
{
    public class DeathMessageProvider : EntityBehavior
    {

        public DeathMessageProvider(Entity entity) : base(entity) { }

        public override string PropertyName() => nameof(DeathMessageProvider);

        private int IgnoreDamageCount = -1;

        private string customDeathMessagePool;
        public string CustomDeathMessagePool 
        { 
            get => customDeathMessagePool; 
            set
            {
                customDeathMessagePool = value;
                if (!string.IsNullOrEmpty(value))
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
                else if(string.IsNullOrEmpty(customDeathMessagePool))
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
}
