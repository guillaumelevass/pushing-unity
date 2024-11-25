using System;
using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using Unity.Profiling;


public class PerformanceTests
{
    private SparrowManager ringLossEffect;

    [SetUp]
    public void SetUp()
    {
        // Load the prefab from the Assets folder
        string prefabPath = "Assets/_Game/Levels/10 Performance Testing/Prefabs/Sparrow.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject instance = GameObject.Instantiate(prefab);
        string ringPrefabPath = "Assets/_Game/Levels/10 Performance Testing/Prefabs/ring.prefab";
        GameObject ringPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(ringPrefabPath);
        ringLossEffect = instance.AddComponent<SparrowManager>();
        ringLossEffect.ringPrefab = ringPrefab;
        // var gameObject = GameObject.Find("Sparrow_LOD0");

    } 
    
    // A Test behaves as an ordinary method
    [Test, Performance]
    public void PerformanceTestsSimplePasses()
    {
        Measure.Method(() =>  ringLossEffect.RingLoss(100)).WarmupCount(10)
            .MeasurementCount(10)
            .IterationsPerMeasurement(5)
            .GC()
            .ProfilerMarkers("CPU Total Frame Time", "GPU Frame Time", "Inl_UniversalRenderTotal") // Compare this with profiler option, create own marker, find marker for update loop, do graphics renderer example, stonks, figure out graphics performance framework
            .SetUp(()=> {/*setup code*/})
            .CleanUp(()=> {/*cleanup code*/})
            .Run();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest, Performance]
    public IEnumerator PerformanceTestsProfileMarker()
    { 
        // Is this the point of the graohics perf tests
        using(Measure.ProfilerMarkers("CPU Total Frame Time", "GPU Frame Time", "Inl_UniversalRenderTotal"))
        {
            for (int i = 0; i < 300; i++)
            {
                ringLossEffect.RingLoss(5000);
                yield return null;

            }
            
        }
    }
}
