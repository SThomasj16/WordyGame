using System;
using UniRx;
using UnityEngine;

namespace Features.Runtime.Scripts
{
    public abstract class RuntimeBehaviour : MonoBehaviour
    {
       public abstract IObservable<Unit> Initialize();
    }
}