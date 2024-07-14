using System;
using UniRx;
using UnityEngine;

namespace Utils.Runtime.Scripts
{
    public abstract class RuntimeBehaviour : MonoBehaviour
    {
       public abstract IObservable<Unit> Initialize();
    }
}