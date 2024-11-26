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
    private FlickyManager flickyManager;

    [SetUp]
    public void SetUp()
    {
        // Load the prefab from the Assets folder
        string prefabPath = "Assets/_Game/Levels/10 Performance Testing/Prefabs/Flicky.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject instance = GameObject.Instantiate(prefab);
        string piopioPrefabPath = "Assets/_Game/Levels/10 Performance Testing/Prefabs/PioPio.prefab";
        GameObject ringPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(piopioPrefabPath);
        flickyManager = instance.AddComponent<FlickyManager>();
        flickyManager.babyPrefab = ringPrefab;
        // var gameObject = GameObject.Find("Sparrow_LOD0");
    } 
    
    // A Test behaves as an ordinary method
    [Test, Performance]
    public void PerformanceTestsSimplePasses()
    {
        Measure.Method(() =>  flickyManager.LoseChirpies(1000)).WarmupCount(10)
            .MeasurementCount(40)
            .IterationsPerMeasurement(5)
            .GC()
             // Compare this with profiler option, create own marker, find marker for update loop, do graphics renderer example, stonks, figure out graphics performance framework, why only editor mode
            .SetUp(()=> {/*setup code*/})
            .CleanUp(()=> {/*cleanup code*/})
            .Run();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest, Performance]
    public IEnumerator PerformanceTestsProfileMarker()
    { 
        using(Measure.ProfilerMarkers("CPU Total Frame Time", "GPU Frame Time", "Inl_UniversalRenderTotal")) // Why No  GPU
        {
            for (int i = 0; i < 300; i++)
            {
                flickyManager.LoseChirpies(5000);
                yield return null;
            }
            
        }
    }
}
