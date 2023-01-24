using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildTower { get; set; } // 타워가 이 타일에 설치 되 있는지?
    private void Awake()
    {
        IsBuildTower = false;
    }
}


// 타워를 설치 가능한 타일 컴포넌트.
// 타워가 지어지면, IsBuildTower = true됨.