using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class PerformanceTests
{
    private SparrowManager ringLossEffect;

    [SetUp]
    public void SetUp()
    {
        var gameObject = GameObject.Find("Sparrow_LOD0");
        ringLossEffect = gameObject.GetComponent<SparrowManager>();


    }
    
    // A Test behaves as an ordinary method
    [Test, Performance]
    public void PerformanceTestsSimplePasses()
    {
        Measure.Method(() =>  ringLossEffect.RingLoss(100)).WarmupCount(10)
            .MeasurementCount(10)
            .IterationsPerMeasurement(5)
            .GC()
            .SetUp(()=> {/*setup code*/})
            .CleanUp(()=> {/*cleanup code*/})
            .Run();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test, Performance]
    public IEnumerator PerformanceTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
