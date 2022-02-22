using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iteractables
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactionText = "Hello";

        public void Interact() => Debug.Log(interactionText);
    }
}
