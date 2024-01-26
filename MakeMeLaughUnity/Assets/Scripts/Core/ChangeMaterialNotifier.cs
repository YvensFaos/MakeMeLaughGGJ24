using System;
using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace Core
{
    public enum ChangeMaterialType
    {
        COLOR, FLOAT, INT
    }
    
    public class ChangeMaterialNotifier : CommandNotifier
    {
        [SerializeField] 
        private Renderer selfRenderer;
        [SerializeField]
        private Material material;
        private Material selfMaterial;
        [SerializeField]
        private ChangeMaterialType type;
        [SerializeField]
        private string property;

        [SerializeField, ShowIf("type", ChangeMaterialType.COLOR)]
        private Color changeToColor;
        private Color originalColor;
        [SerializeField, ShowIf("type", ChangeMaterialType.FLOAT)]
        private float changeToFloat;
        private float originalFloat;
        [SerializeField, ShowIf("type", ChangeMaterialType.INT)]
        private int changeToInt;
        private int originalInt;

        private void Awake()
        {
            AssessUtils.CheckRequirement(ref selfRenderer, this);

            selfMaterial = new Material(material);
            selfRenderer.material = selfMaterial;

            switch (type)
            {
                case ChangeMaterialType.COLOR:
                    originalColor = selfMaterial.GetColor(property);
                    break;
                case ChangeMaterialType.FLOAT:
                    originalFloat = selfMaterial.GetFloat(property);
                    break;
                case ChangeMaterialType.INT:
                    originalFloat = selfMaterial.GetInt(property);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void NotifyMeDown(GameObject caller)
        {
            switch (type)
            {
                case ChangeMaterialType.COLOR:
                    selfMaterial.SetColor(property, changeToColor);
                    break;
                case ChangeMaterialType.FLOAT:
                    selfMaterial.SetFloat(property, changeToFloat);
                    break;
                case ChangeMaterialType.INT:
                    selfMaterial.SetInt(property, changeToInt);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void NotifyMeUp(GameObject caller)
        {
            switch (type)
            {
                case ChangeMaterialType.COLOR:
                    selfMaterial.SetColor(property, originalColor);
                    break;
                case ChangeMaterialType.FLOAT:
                    selfMaterial.SetFloat(property, originalFloat);
                    break;
                case ChangeMaterialType.INT:
                    selfMaterial.SetInt(property, originalInt);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}