using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RandomGenerators
{
    public class RandomGenerator<T>
    {
        public enum Type
        {
            Pure,
            Weight
        }

        private Type type;
        private T[] data;
        private float[] weights;
        private float totalweight = 0;
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="type">Generator type: Pure, Weight</param>
        /// <param name="data">Data that will be randomly choose</param>
        public RandomGenerator(Type type, Dictionary<T, float> data)
        {
            this.type = type;
            this.data = data.Keys.ToArray();
            this.weights = data.Values.ToArray();

            CalculateTotalWeight(weights);
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="type">Generator type: Pure, Weight</param>
        /// <param name="data">Data that will be randomly choose</param>
        /// <param name="weights">Optional: Weights for data</param>
        public RandomGenerator(Type type, T[] data, float[] weights = null)
        {
            this.type = type;
            this.data = data;
            this.weights = weights;
            if ((weights == null || weights.Length != data.Length) && type == Type.Weight)
                Debug.LogError("You have to specify weights for data for this type of generator.");

            CalculateTotalWeight(weights);
        }

        private void CalculateTotalWeight(float[] weights)
        {
            foreach (var weight in weights)
            {
                if (weight > 0)
                    totalweight += weight;
                else
                    Debug.LogWarning("All negative weights will be used as zero.");
            }
        }


        public void SetTypeAndWeight(Type newType, float[] weights)
        {
            if (type == Type.Weight && (weights == null || weights.Length != data.Length))
            {
                Debug.LogError("You have to specify weights for data for this type of generator.");
                return;
            }
            if(type == Type.Weight && weights.Length != data.Length)
            {
                Debug.LogError("You have to have the same length of weights and data.");
                return;
            }

            type = newType;
            this.weights = weights;
        }

        public void SetType(Type newType)
        {
            type = newType;
            if ((weights == null || weights.Length != data.Length) && type == Type.Weight)
                Debug.LogError("You have to specify weights for data for this type of generator.");
        }

        public void SetWeights(float[] weights)
        {
            this.weights = weights;
        }

        public T Generate()
        {
            switch (type)
            {
                case Type.Pure:
                    return PureGeneration();
                case Type.Weight:
                    return WeightGeneration();
                default:
                    return PureGeneration();
            }
        }

        private T PureGeneration()
        {
            return data[Random.Range(0, data.Length)];
        }

        private T WeightGeneration()
        {
            var randomValue = Random.Range(0, totalweight);
            var index = FindIndexByWeight(randomValue, weights);
            return data[index];
        }

        private int FindIndexByWeight(float randomValue, float[] weights)
        {
            var currentWeight = 0f;
            for(var i =0;i<weights.Length;++i)
            {
                currentWeight += weights[i];
                if (currentWeight >= randomValue)
                    return i;
            }
            return weights.Length - 1;
        }
    }
}
