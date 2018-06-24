using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine.TestTools;
using Debug = UnityEngine.Debug;

namespace RandomGenerators.Tests
{
    public class RandomIntGeneratorTest
    {
        [TestCase(1000)]
        [TestCase(100)]
        [TestCase(10)]
        public void RandomIntGeneratorPureTest(int sessions)
        {
            var rsp = new Dictionary<int, float> {{0, 1f}, {1, 1f}, {2, 1f}};
            var generator = new RandomGenerator<int>(RandomGenerator<int>.Type.Pure, rsp);
            var counter = new int[3];

            for (var i = 0; i < sessions; i++)
            {
                var randomValue = generator.Generate();
                counter[randomValue]++;
            }

            for (var i = 0; i < counter.Length; i++)
            {
                Debug.Log($"Current index {i} with count {counter[i]}");
            }
        }

        [TestCase(1000)]
        [TestCase(100)]
        [TestCase(10)]
        public void RandomIntGeneratorWeightTest(int sessions)
        {
            var rsp = new Dictionary<int, float> { { 0, 0.1f }, { 1, 0.6f }, { 2, 0.3f } };
            var generator = new RandomGenerator<int>(RandomGenerator<int>.Type.Weight, rsp);
            var counter = new int[3];

            for (var i = 0; i < sessions; i++)
            {
                var randomValue = generator.Generate();
                counter[randomValue]++;
            }

            for (var i = 0; i < counter.Length; i++)
            {
                Debug.Log($"Current index {rsp[i]} with count {counter[i]}");
            }
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator RandomIntGeneratorTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}
