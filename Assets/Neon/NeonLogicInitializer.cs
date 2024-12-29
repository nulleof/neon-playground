using UnityEngine;
using ME.BECS;

namespace Neon {

    [DefaultExecutionOrder(-100)]
    public class NeonLogicInitializer : WorldInitializer {

        public static NeonLogicInitializer instance;

        protected override void DoWorldAwake() {

            NeonLogicInitializer.instance = this;

            this.world.parent = NeonLogicInitializer.instance.world;

            base.DoWorldAwake();

        }

    }
    
}