using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildTower { get; set; } // Ÿ���� �� Ÿ�Ͽ� ��ġ �� �ִ���?
    private void Awake()
    {
        IsBuildTower = false;
    }
}


// Ÿ���� ��ġ ������ Ÿ�� ������Ʈ.
// Ÿ���� ��������, IsBuildTower = true��.