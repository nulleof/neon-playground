using ME.BECS;

namespace Neon.FeaturesClient.Camera.Components {

    public struct CameraAnchorStorage : IComponent {

        public EquatableDictionaryAuto<Ent, Ent> watchingEntries;

    }

    public struct CameraAnchor : IComponent { }

}
