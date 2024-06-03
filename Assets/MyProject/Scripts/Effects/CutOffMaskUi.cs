using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutOffMaskUi : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material _material = new Material(base.materialForRendering);
            _material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return _material;
        }
    }
}
