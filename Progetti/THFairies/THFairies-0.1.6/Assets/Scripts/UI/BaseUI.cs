using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseUI : MonoBehaviour
{
    public abstract void Show(Action callback);

    public abstract void Hide(Action callback);
}
