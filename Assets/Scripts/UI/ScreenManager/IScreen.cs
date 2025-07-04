using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreen
{
    void Activate();
    void Deactivate();
    void Release();
}
