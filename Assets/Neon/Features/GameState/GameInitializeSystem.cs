using ME.BECS;
using UnityEngine;

namespace Neon.Systems {

    public struct GameInitializeSystem : IAwake, IDestroy {

        public void OnAwake(ref SystemContext context) {

            Debug.Log($"{typeof(GameInitializeSystem)} initialize");

        }

        public void OnDestroy(ref SystemContext context) {
            Debug.Log($"{typeof(GameInitializeSystem)} destroy");
        }

    }

}
