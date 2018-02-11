using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBehaviour : MonoBehaviour {

    public List<GameObject> phoneObjects;
    private bool cameraActive = false;

    public Image cameraFlash;

	void Start () {
        SetCameraActive(false);	
	}
	
	
	void Update () {
        if(Input.GetMouseButton(1) && !cameraActive)
        {
            SetCameraActive(true);
        }
        else if(cameraActive && !Input.GetMouseButton(1))
        {
            SetCameraActive(false);
        }

        if (cameraActive && !Input.GetMouseButton(0))
        {
            StartCoroutine(CameraFlash());
        }
	}

    void SetCameraActive(bool active)
    {
        cameraActive = active;

        foreach(var obj in phoneObjects)
        {
            obj.SetActive(active);
        }
    }

    IEnumerator Fade(float start, float end, float length, Image currentObject)
    {
        if(currentObject.color.a == start)
        {
            Color curColor;
            for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime * (1/length))
            {
                /* 컬러 속성을 직접 조절할 수 없으므로 복사본을 만든다.*/
                curColor = currentObject.color;

                curColor.a = Mathf.Lerp(start, end, i);

                currentObject.color = curColor;

                yield return null;
            }
            curColor = currentObject.color;
            /* 페이드가 완전히 끝났는지 확인한다. (반올림 오류때문에 lerp 결과가 항상 같은 값으로 끝나지 않기 때문이다.) */

            curColor.a = end;
            currentObject.color = curColor;
        }
    }

    IEnumerator CameraFlash()
    {
        yield return StartCoroutine(Fade(0.0f, 0.8f, 0.2f, cameraFlash));
        yield return StartCoroutine(Fade(0.0f, 0.0f, 0.2f, cameraFlash));
        StopCoroutine("CameraFlash");
    }
}
