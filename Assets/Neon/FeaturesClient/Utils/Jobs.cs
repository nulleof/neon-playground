using ME.BECS;
using ME.BECS.Jobs;
using BURST = Unity.Burst.BurstCompileAttribute;

namespace Neon.FeaturesClient.Utils {

    [BURST(CompileSynchronously = true)]
    public struct DestroyJob : IJobForComponents {

        public void Execute(in JobInfo jobInfo, in Ent ent) {

            ent.Destroy();

        }

    }
}
