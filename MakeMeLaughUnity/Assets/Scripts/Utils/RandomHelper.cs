using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class RandomHelper<T>
    {
        public static T GetRandomFromList(List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }

    public static class RandomChancePair<T>
    {
        public static T GetRandomFromChanceList(List<Tuple<T, float>> pairs, T defaultResult)
        {
            var total = pairs.Sum(p => p.Item2);
            var current = 0.0f;
            var chance = Random.Range(0.0f, total);

            foreach (var pair in pairs)
            {
                if (chance <= pair.Item2 + current)
                {
                    return pair.Item1;
                }

                current += pair.Item2;
            }

            return defaultResult;
        }
    }

    public static class RandomChanceUtils
    {
        public static bool GetChance(float upTo, float max = 100.0f)
        {
            return Random.Range(0, max) <= upTo;
        }

        public static float GetRandom(float upTo)
        {
            return Random.Range(0, upTo);
        }
        
        public static float GetRandomInRange(Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        public static int GetRandomInRangeAsInt(Vector2 range)
        {
            return (int)GetRandomInRange(range);
        }
    }

    public static class RandomPointUtils
    {
        public static Vector3 GetRandomPointWithBox(BoxCollider boxCollider)
        {
            var bounds = boxCollider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
        }
        
        public static Vector2 GetRandomPointWithBox2D(BoxCollider2D boxCollider2D)
        {
            var bounds = boxCollider2D.bounds;
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));
        }

        public static Vector3 GetRandomPointWithinCircleCollider2D(CircleCollider2D circleCollider)
        {
            var randomPoint2D = (Vector2)circleCollider.bounds.center + Random.insideUnitCircle * circleCollider.radius;
            return new Vector3(randomPoint2D.x, randomPoint2D.y, 0.0f);
        }
        
        public static Vector2 GenerateRandomDirection2D()
        {
            var angle = Random.Range(0f, Mathf.PI * 2f);
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        
        public static Vector3 GenerateRandomDirection2Din3D()
        {
            var angle = Random.Range(0f, Mathf.PI * 2f);
            return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
        }
    }
}