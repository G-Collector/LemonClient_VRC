using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC;
using VRC.Core;
using VRC.Udon;
using VRC.SDKBase;
using MelonLoader;
using UnityEngine.UI;
using VRC.UI.Elements.Menus;
using System.Collections;
using VRC.SDK3.Components;

namespace _LemonClient.Features
{
    class ESPHandler
    {
        public static IEnumerator ItemESP()
        {
            List<Renderer> itemsFound = new List<Renderer>();
            Transform[] allObjects = Resources.FindObjectsOfTypeAll<Transform>();
            Transform[] array = allObjects;
            foreach (Transform transform in array)
            {
                itemsFound.Add(transform.GetChild(0).gameObject.GetComponent<Renderer>());
            }
            Transform[] array2 = null;
            for (; ; )
            {
                foreach (Renderer renderer in itemsFound)
                {
                    bool flag2 = renderer != null;
                    if (flag2)
                    {
                        bool activeInHierarchy = renderer.gameObject.activeInHierarchy;
                        if (activeInHierarchy)
                        {
                            HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, true);
                        }
                        else
                        {
                            HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, false);
                        }
                    }
                    yield return new WaitForSeconds(0.2f);
                    //renderer = null;
                }
                List<Renderer>.Enumerator enumerator = default(List<Renderer>.Enumerator);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
