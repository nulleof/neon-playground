using ME.BECS;
using ME.BECS.Transforms;

namespace Neon.Systems {

    public struct MapInitializeSystem : IAwake {

        public ObjectReference<EntityConfig> ground;
        public ObjectReference<EntityConfig> unit;

        public void OnAwake(ref SystemContext context) {

            var groundEnt = Ent.New(context, editorName: "ground");
            this.ground.Value.Apply(groundEnt);

            var unitEnt = Ent.New(context, editorName: "unit");
            this.unit.Value.Apply(unitEnt);

        }

    }

}
