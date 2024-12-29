using BURST = Unity.Burst.BurstCompileAttribute;
using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Transforms;
using Neon.Components;
using Neon.FeaturesClient.Camera.Components;
using Neon.FeaturesClient.Utils;

namespace Neon.FeaturesClient.Camera.Systems {

#region Jobs

    [BURST(CompileSynchronously = true)]
    public struct CleanCameraAnchorsStorageJob : IJobForComponents<CameraAnchorStorage> {

        public void Execute(in JobInfo jobInfo, in Ent ent, ref CameraAnchorStorage c0) {

            foreach (var entry in c0.watchingEntries) {

                // No alive anchors. Remove this line
                if (entry.value.IsAlive() == false) {
                    c0.watchingEntries.Remove(entry.key);
                    continue;
                }

                if (entry.key.IsAlive() == false) {

                    if (entry.value.IsAlive() == true) entry.value.Destroy();
                    c0.watchingEntries.Remove(entry.key);
                    continue;

                }

            }

        }

    }

    [BURST(CompileSynchronously = true)]
    public struct UpdateCameraAnchorsJob : IJobForComponents {

        public World visualWorld;
        public CameraAnchorStorage anchorStorage;

        public void Execute(in JobInfo jobInfo, in Ent ent) {

            if (this.anchorStorage.watchingEntries.ContainsKey(ent) == true) return;

            var anchorEntity = this.visualWorld.NewEnt();
            anchorEntity.Set(new CameraAnchor());
            anchorEntity.SetParent(ent);
            this.anchorStorage.watchingEntries.Add(ent, anchorEntity);

        }

    }



#endregion

    [EditorComment("Searches if there are entities in the world that have no anchors yet")]
    [BURST(CompileSynchronously = true)]
    public struct CreateCameraAnchorsSystem : IUpdate, IAwake, IDestroy {

        private Ent cameraAnchorsStorage;

        [BURST(CompileSynchronously = true)]
        public void OnAwake(ref SystemContext context) {

            var storageEnt = Ent.New(in context, editorName: "cameraAnchorsStorage");
            storageEnt.Set(new CameraAnchorStorage() {
                watchingEntries = new EquatableDictionaryAuto<Ent, Ent>(storageEnt, 100),
            });
            this.cameraAnchorsStorage = storageEnt;

        }

        [BURST(CompileSynchronously = true)]
        public void OnUpdate(ref SystemContext context) {

            var logicWorld = context.world.parent;

            // clean storage
            var dependsOn = API.Query(in context, context.dependsOn).With<CameraAnchorStorage>()
                .Schedule<CleanCameraAnchorsStorageJob, CameraAnchorStorage>(new CleanCameraAnchorsStorageJob());

            // create & update anchors
            dependsOn = API.Query(in logicWorld, dependsOn).With<CanAnchorCamera>().Schedule<UpdateCameraAnchorsJob>(
                new UpdateCameraAnchorsJob() {
                    visualWorld = context.world,
                    anchorStorage = this.cameraAnchorsStorage.Read<CameraAnchorStorage>(),
                });

            context.SetDependency(dependsOn);

        }

        [BURST(CompileSynchronously = true)]
        public void OnDestroy(ref SystemContext context) {

            var dependsOn = API.Query(in context, context.dependsOn).With<CameraAnchor>()
                .Schedule(new DestroyJob());
            dependsOn = API.Query(in context, dependsOn).With<CameraAnchorStorage>()
                .Schedule(new DestroyJob());

            context.SetDependency(dependsOn);

        }

    }

}
