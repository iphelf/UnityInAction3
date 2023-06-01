using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public class WebLoadingBillboard : MonoBehaviour, IOperatee
    {
        public void Operate()
        {
            Managers.Images.GetWebImage(OnWebImage);
        }

        private void OnWebImage(Texture2D image)
        {
            GetComponent<Renderer>().material.mainTexture = image;
        }
    }
}