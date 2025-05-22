using System.Collections.Generic;
using UnityEngine;

namespace Block
{
    public class BlockGraphics : MonoBehaviour
    {
        [SerializeField] private float      maxIntensity;
        [SerializeField] private Light      mainLight;
        [SerializeField] private Material   ceilingMat;
        [SerializeField] private Material   ceilingFogMat;
        [SerializeField] private Material   floorMat;
        [SerializeField] private GameObject floorCeiling;
    
        #region Shader
        private void SetMatDark()
        {
            var materials = new List<Material>()
            {
                floorMat,
                ceilingFogMat
            };
            floorCeiling.GetComponent<Renderer>().SetMaterials(materials);
        }
        private void SetMatLight()
        {
            var materials = new List<Material>() {
                floorMat,
                ceilingMat
            };
            floorCeiling.GetComponent<Renderer>().SetMaterials(materials);
        }
        #endregion
        public void Setup(bool isFogBlock)
        {
            if (!isFogBlock)
            {
                SetMatLight();
                mainLight.intensity = maxIntensity;        
            }
            else
            {
                SetMatDark();
                mainLight.intensity = 0.0f;
            }
        }
        public void OnIncreaseCount(int playerCount)
        {
            switch (playerCount)
            {
                case 1:
                    mainLight.intensity /= 2;
                    break;
                case 2:
                    mainLight.intensity /= 1.5f;
                    break;
                case 3:
                    SetMatDark();
                    mainLight.intensity = 0.0f;
                    break;
            }
        }

        public void ChangeLightIntensity(float intensity)
        {
            if (intensity == 0.0f)
                mainLight.gameObject.SetActive(false);
            mainLight.intensity = intensity;
        }
    }
}