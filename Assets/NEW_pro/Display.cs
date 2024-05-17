using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DualMonitorSetup : MonoBehaviour
{
    public Text text_diplayCount;

    void Start()
    {
        // 디스플레이 수 확인
        int displayCount = Display.displays.Length;

        /*
        // 각각의 디스플레이에 카메라 설정
        if (displayCount >= 2)
        {
            // 디스플레이 1에 카메라 1 출력
            Camera.main.targetDisplay = 0;
            Camera secondCamera = gameObject.AddComponent<Camera>();
            secondCamera.targetDisplay = 1; // 디스플레이 2에 카메라 2 출력
        }
        //위 코드에서 오류 발생
        */


        //text_diplayCount.text = "displayCount: " + displayCount;

        if (displayCount >= 2)
        {
            Display.displays[1].Activate();
        }

    }

}