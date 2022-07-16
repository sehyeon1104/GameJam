using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static TimerEnum;
public class ArrowBlock : MonoBehaviour
{
    //화살방향 // bool같은것을 받아가지고
    
    //체스가 내방향에 있다면 성공
    //닿았는데 체스가 내방향에 없다면 데미지 달게
    public ArrowRotate arrowRotate;
    public AudioSource audioSource;
    public ParticleSystem particle;


    public TimerChek timerCheck;
    public GameObject testing;

    bool isActive = false;
    public GameObject spriteK;
    public GameObject spriteArrow;
    public GameObject breakKing;

    //public UnityEngine.Rendering.Universal.Light2D king;

    //인터페이스로 그걸 만들까 
    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (isActive == true) yield break;
        Debug.Log("닿았다");
        //Debug.Log(collision.gameObject.GetComponent<IArrow>().GetArrowState());
        if (collision.gameObject.CompareTag("Chess") )
        {
            IArrow arr = collision.gameObject.GetComponent<IArrow>();


            Debug.Log("에너미 arrow " + arr.GetArrowState());
            if(arr != null)
            {
                if(arrowRotate.arrow == arr.GetArrowState())
                {

                    particle.Play();
                    collision.gameObject.SetActive(false);


                    //적이 막거나
                }
                else
                {

                    //플레이어 죽음 이벤트 큐 발생
                    switch (timerCheck)
                    {
                        case TimerChek.easy:
                            Timer.Instance.copyEasyCheckTimer();
                            testing.GetComponent<Testing_E>().isSpawn = false;
                            if (Timer.Instance.easyCheckTimer > TimePlayerpersManager.Instance.GetCheckEasyLoad())
                            {


                                TimePlayerpersManager.Instance.SaveEasy();
                            }
                            break;
                        case TimerChek.normal:

                            testing.GetComponent<Testing>().isSpawn = false;
                            Timer.Instance.copyNormalCheckTimer();
                            if (Timer.Instance.normalCheckTimer > TimePlayerpersManager.Instance.GetCheckLoad())
                            {


                                TimePlayerpersManager.Instance.SaveNormal();
                            }
                            break;
                        case TimerChek.hard:

                            testing.GetComponent<Testing_H>().isSpawn = false;
                            Timer.Instance.copyHardCheckTimer();
                            if (Timer.Instance.hardCheckTimer > TimePlayerpersManager.Instance.GetCheckHardLoad())
                            {


                                TimePlayerpersManager.Instance.SaveHard();
                            }
                            break;
                        default:
                            break;
                    }


                    GameEvents.current.playerHpHealthTriggerEnter(); //데드틱
                    isActive = true;
                    collision.gameObject.SetActive(false);
                    audioSource.Stop();
                    GameManager.Instance.TimeScale = 0f;

         

                    //화면 가까이 하는 코드
                    spriteArrow.SetActive(false);
                    yield return StartCoroutine(CameraZoooooooooom.Instance.CameraZoom());
                    
                    transform.parent.gameObject.transform.DOShakePosition(0.4f, 0.2f, 24, 1f, false, true).OnComplete(()=>
                    {
                        //함수 호출해가지고 
                        //transform.parent.gameObject.SetActive(false); //플레이어 펄스(삭제와 같은)

                        
                        spriteK.SetActive(false);

                        GameObject obj = Instantiate(breakKing, transform.position, Quaternion.identity);
     
                    });

                    //

                }

            }
         

        }
    }

}
