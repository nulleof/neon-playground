using ME.BECS;
using ME.BECS.Transforms;

namespace Neon.Systems {

    public struct MapInitializeSystem : IAwake {

        public ObjectReference<EntityConfig> ground;

        public void OnAwake(ref SystemContext context) {

            var groundEnt = Ent.New(context, editorName: "Ground");
            groundEnt.GetOrCreateAspect<TransformAspect>();
            this.ground.Value.Apply(groundEnt);

        }

    }

}
