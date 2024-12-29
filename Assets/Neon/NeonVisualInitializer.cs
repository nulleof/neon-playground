using UnityEngine;
using ME.BECS;

namespace Neon {

    [DefaultExecutionOrder(-80)]
    public class NeonVisualInitializer : WorldInitializer {

        public static NeonVisualInitializer instance;

        protected override void DoWorldAwake() {

            NeonVisualInitializer.instance = this;

            this.world.parent = NeonLogicInitializer.instance.world;

            base.DoWorldAwake();

        }

    }
    
}