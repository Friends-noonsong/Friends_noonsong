using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TalkDialog : MonoBehaviour
{
    public DialogManager DialogManager;
    public Animator noonDungAnimator; // NoonDung 오브젝트의 Animator
    public GameObject MovingObject; // 좌측 상단에 있는 3D 오브젝트
    public Transform arCamera; // AR 카메라 Transform
    public float moveDuration = 2f; // 이동 애니메이션 지속 시간

    private Vector3 startPos;
    private Vector3 endPos;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    private bool[] dialogTriggered = new bool[4]; // 다이얼로그가 호출되었는지 여부를 저장

    // 첫 번째 대화 설정
    public void FirstDialog()
    {
        if (dialogTriggered[0]) return;

        dialogTriggered[0] = true;

        var FirstDialog = new List<DialogData>();

        FirstDialog.Add(new DialogData("/color:black/숙명여대에 갓 입학한 새송이는 학교 탐방을 오게 되었다!"));
        FirstDialog.Add(new DialogData("/color:black/그런데 어쩌지? 학교가 너무 복잡해!"));
        FirstDialog.Add(new DialogData("/color:black/[학교가 너무 처음이라 막막하네...]", "User", () => StartCoroutine(NoonDungComing()))); // 애니메이션 실행 및 대사 출력
        FirstDialog.Add(new DialogData("/color:black//wait:0.5/안녕, 친구야! 혹시 무슨 고민 있어?", "NoonDung", () => ChangeAnimation("fast")));
        FirstDialog.Add(new DialogData("/color:black/[(사정을 설명한다.)]", "User"));
        FirstDialog.Add(new DialogData("/color:black/아하, 아직 학교가 처음이라 모르는 게 많다고? 음.. 어디보자~", "NoonDung", () => ChangeAnimation("stand")));
        FirstDialog.Add(new DialogData("/color:black/그렇지! 숙명여대라면 역시 눈송이! 그 애가 널 도와줄 수 있을 거야!", "NoonDung", () => ChangeAnimation("move")));
        FirstDialog.Add(new DialogData("/color:black/같이 학교를 돌아다니면서 /color:blue/눈송이/color:black/가 어디에 있는지 찾아보자!/wait:1//close/", "NoonDung"));
        FirstDialog.Add(new DialogData("/color:black//wait:1/[저기 하늘에 떠 다니는 건 뭐지?]", "User"));
        FirstDialog.Add(new DialogData("/color:black/어디? 어디?", "NoonDung"));
        FirstDialog.Add(new DialogData("/color:black//wait:1/우리는! /wait:0.5/학교를 지키는 어벤져스, /click/눈꽃송이들이야!", "NoonKkot"));
        FirstDialog.Add(new DialogData("/color:black/마침 잘 만났다! 얘들아, 이 새송이가 눈송이와 친구가 되고 싶대!", "NoonDung"));
        FirstDialog.Add(new DialogData("/color:black/그런거라면... 눈송이는 눈의 결정을 좋아하니까, 눈의 결정을 준다면 분명 친구가 될 수 있을 거야", "NoonKkot"));
        FirstDialog.Add(new DialogData("/color:black/마침 우리한테 꿍쳐놓은 눈의 결정이 있으니까, 너한테 줄게!", "NoonKkot"));
        FirstDialog.Add(new DialogData("/color:black/어려운 친구를 돕는 것도 우리 일이니까. 우리가 새송이를 도와주는 건 어떨까? /click/(뭉치면 산다!)", "NoonKkot"));
        FirstDialog.Add(new DialogData("/color:black/그래, 눈의 결정이라면 우리가 전문이니까, 함께 다니면서 눈의 결정 찾는걸 도와줄게! /click/(맡겨 줘!)", "NoonKkot"));
        FirstDialog.Add(new DialogData("/color:black/[고마워, 눈꽃송이들!]/wait:1//close/", "User"));
        FirstDialog.Add(new DialogData("/color:black//wait:1/앗! 찾았다", "RoRo"));
        FirstDialog.Add(new DialogData("/color:black/[앗!]", "User"));
        FirstDialog.Add(new DialogData("/color:black/네가 눈송이를 찾아 다닌다는 새송이 맞지! 소식을 듣고 한달음에 달려왔어!", "RoRo"));
        FirstDialog.Add(new DialogData("/color:black/로로잖아! 과연 학교의 소식통이라 그런지, 소식이 빠르네!", "NoonDung", () => ChangeAnimation("Surprised")));
        FirstDialog.Add(new DialogData("/color:black//emote:Happy/히히, 1캠퍼스 정문에서 눈송이를 본 것 같다는 걸 알려주려고! 새송이가 누군지 궁금하기도 했고!", "RoRo"));
        FirstDialog.Add(new DialogData("/color:black//emote:Call/아직 학교 지리는 잘 모르지? 내가 같이 가줄게!", "RoRo"));

        DialogManager.Show(FirstDialog);
    }

    public void SecondDialog()
    {
        if (dialogTriggered[1]) return;
        if (!dialogTriggered[0]) return; // 이전 다이얼로그가 호출되지 않았으면 return

        dialogTriggered[1] = true;

        var SecondDialog = new List<DialogData>();

        SecondDialog.Add(new DialogData("[저기 하늘에 떠 다니는 건 뭐지?]", "User"));
        SecondDialog.Add(new DialogData("저기,, 안녕하세요! 처음 보는 분이네요..!", "NoonGyeol"));
        SecondDialog.Add(new DialogData("눈결이 안녕! 혹시 근처에서 눈송이 못봤어?", "NoonDung"));
        SecondDialog.Add(new DialogData("눈송이 말인가요? 음... 못 봤어요. 무슨 일이신데요?", "NoonGyeol"));
        SecondDialog.Add(new DialogData("[(눈결이에거 사정을 설명한다)]", "User"));
        SecondDialog.Add(new DialogData("앗, 그렇다면 이게 도움이 될 거예요!", "NoonDung"));
        SecondDialog.Add(new DialogData("저는 지도를 잘 보거든요. 그 외에 이것저것 많은 것을 알고 있으니까, 제 지식이 도움이 될 수 있을 것 같아요.", "NoonGyeol"));
        SecondDialog.Add(new DialogData("[(그럼 혹시 도와줄 수 있냐고 묻는다.)]", "User"));
        SecondDialog.Add(new DialogData("물론이에요..! 저도 동행할게요.", "NoonGyeol"));
        SecondDialog.Add(new DialogData("으~음.. 눈송이 대신 눈결이가 있었네. 괜찮아! 마침 한 곳 더 짐작이 가는 곳이 있어!", "RoRo"));
        SecondDialog.Add(new DialogData("2캠퍼스 정문으로 가보자!", "RoRo"));

        DialogManager.Show(SecondDialog);
    }

    public void ThirdDialog()
    {
        if (dialogTriggered[2]) return;
        if (!dialogTriggered[1]) return; // 이전 다이얼로그가 호출되지 않았으면 return

        dialogTriggered[2] = true;

        var ThirdDialog = new List<DialogData>();

        ThirdDialog.Add(new DialogData("안녕, 친구들? 처음 보는 친구도 있구나! 새송이인가 보네?", "KkotSong"));
        ThirdDialog.Add(new DialogData("이 친구는 꽃송이야! 눈송이의 베프인 꽃송이라면 눈송이가 어디 있는지 알 지도 몰라!", "RoRo"));
        ThirdDialog.Add(new DialogData("눈송이? 너희 눈송이를 찾고 있니?", "KkotSong"));
        ThirdDialog.Add(new DialogData("맞아요. 새송이가 눈송이와 친구가 되고 싶대요.", "NoonGyeol"));
        ThirdDialog.Add(new DialogData("그렇다면 정확히 찾아왔어. 마침 방금 전까지 눈송이랑 함께 있던 참이었거든.", "KkotSong"));
        ThirdDialog.Add(new DialogData("아마 눈송이는 /color:blue/프라임관/color:black/에 있을 거야! 어딘지 아니? 같이 가 줄게.", "KkotSong"));

        DialogManager.Show(ThirdDialog);
    }

    public void ForthDialog()
    {
        if (dialogTriggered[3]) return;
        if (!dialogTriggered[2]) return; // 이전 다이얼로그가 호출되지 않았으면 return

        dialogTriggered[3] = true;

        var ForthDialog = new List<DialogData>();

        ForthDialog.Add(new DialogData("찾았다, 눈송이!", "NoonKkot"));
        ForthDialog.Add(new DialogData("안녕, 친구들! 어라, 처음 보는 친구도 있네?", "NoonSong"));
        ForthDialog.Add(new DialogData("이 애가 너와 친구가 되고 싶다고 해서 데려왔어!", "NoonDung"));
        ForthDialog.Add(new DialogData("눈송이를 위한 선물도 가져왔어! (두근두근…!)", "NoonKkot"));
        ForthDialog.Add(new DialogData("와아, 눈의 결정이네! 정말 기뻐!", "NoonSong"));
        ForthDialog.Add(new DialogData("이렇게 찾아와 줘서 고마워, 그럼 우리 오늘부터 친구 하자!", "NoonSong"));
        ForthDialog.Add(new DialogData("[(눈송이와 친구가 되자.)]"));
        ForthDialog.Add(new DialogData("앗-! 다들 나만 빼고 여기 모여 있었구나!", "Turi"));
        ForthDialog.Add(new DialogData("어라? 못 보던 얼굴도 있네?", "Turi"));
        ForthDialog.Add(new DialogData("[(인사한다.)]", "Turi"));
        ForthDialog.Add(new DialogData("안녕, 튜리! 이 애는 새로운 눈송이인데, 나랑 친구가 되고 싶다고 찾아와 줬어!", "NoonSong"));
        ForthDialog.Add(new DialogData("오… 이해했어!", "Turi"));
        ForthDialog.Add(new DialogData("거기 눈송, 좀 더 다양한 눈송이를 만나보고 싶지 않아?", "Turi"));
        ForthDialog.Add(new DialogData("[(고개를 끄덕인다.)]", "User"));
        ForthDialog.Add(new DialogData("흐흥, 미래 산업시대의 선구자가 될 이 튜리님에게 너 같은 친구들을 위한 발명품이 있지!", "Turi"));
        ForthDialog.Add(new DialogData("바로 /color:blue/특별한 눈의 결정/color:black/! 특별한 눈송이들의 마음도 사로잡을 수 있는 대단한 아이템이야!", "Turi"));
        ForthDialog.Add(new DialogData("어때? 가지고 싶지? 그 대신, 특별한 눈의 결정을 만들기 위해서는 눈의 결정이 많이 필요해.", "Turi"));
        ForthDialog.Add(new DialogData("/color:blue/제2캠퍼스 원형 광장/color:black/에 있는 내 연구실에 오면 다양한 특별한 눈의 결정으로 바꿔줄게! 잊지 말고 놀러 와~!", "Turi"));
        ForthDialog.Add(new DialogData("튜토리얼을 성공적으로 마친 당신에게 이것을 드립니다!"));
        ForthDialog.Add(new DialogData("다양한 눈송이들과 친구가 되어 완벽한 학생증을 완성하는 것을 목표로 힘을 내 봐요!"));
        ForthDialog.Add(new DialogData("도감을 다 채우고 학생증을 완성한다면, 특별한 선물이 있을 지도…!?"));

        DialogManager.Show(ForthDialog);
    }


    private void Start()
    {
        FirstDialog();
    }

    private void ChangeAnimation(string trigger)
    {
        noonDungAnimator.SetTrigger(trigger); // 애니메이션 트리거 설정
    }

    public bool IsDialogTriggered(int index)
    {
        if (index >= 0 && index < dialogTriggered.Length)
        {
            return dialogTriggered[index];
        }
        return false;
    }

    private IEnumerator NoonDungComing()
    {
        // 시작 위치와 끝 위치 설정
        Vector3 startPos = arCamera.TransformPoint(new Vector3(-1.5f, 1.5f, 5f)); // AR 카메라의 좌측 상단 (화면 좌표계)
        Vector3 endPos = arCamera.TransformPoint(new Vector3(0f, 0f, 2f)); // AR 카메라의 정면 (화면 중앙)

        // 눈덩이 초기 위치 설정
        MovingObject.transform.position = startPos;

        // 이동 애니메이션 실행
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            MovingObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // 정확한 최종 위치로 설정
        MovingObject.transform.position = endPos;

        // 이동이 완료된 후 다음 대화로 넘어가기
        DialogManager.Click_Window(); // 다음 대사로 넘어가기 위해 Click_Window 메서드 호출
    }
}