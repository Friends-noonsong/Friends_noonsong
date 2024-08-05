using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TalkDialog : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Animation;

    private void FirstDialog()
    {
        var FirstDialog = new List<DialogData>();

        FirstDialog.Add(new DialogData("/color:black/숙명여대에 갓 입학한 새송이는 학교 탐방을 오게 되었다!"));

        FirstDialog.Add(new DialogData("/color:black/그런데 어쩌지? 학교가 너무 복잡해!"));

        FirstDialog.Add(new DialogData("/color:black/[학교가 너무 처음이라 막막하네...]", "User"));

        FirstDialog.Add(new DialogData("/color:black//wait:0.5/안녕, 친구야! 혹시 무슨 고민 있어?", "NoonDung", () => Show_Animation(0)));

        FirstDialog.Add(new DialogData("/color:black/[(사정을 설명한다.)]", "User"));

        FirstDialog.Add(new DialogData("/color:black/아하, 아직 학교가 처음이라 모르는 게 많다고? 음.. 어디보자~", "NoonDung"));

        FirstDialog.Add(new DialogData("/color:black/그렇지! 숙명여대라면 역시 눈송이! 그 애가 널 도와줄 수 있을 거야!", "NoonDung"));

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

        FirstDialog.Add(new DialogData("/color:black/로로잖아! 과연 학교의 소식통이라 그런지, 소식이 빠르네!", "NoonDung"));

        FirstDialog.Add(new DialogData("/color:black//emote:Happy/히히, 1캠퍼스 정문에서 눈송이를 본 것 같다는 걸 알려주려고! 새송이가 누군지 궁금하기도 했고!", "RoRo"));

        FirstDialog.Add(new DialogData("/color:black//emote:Call/아직 학교 지리는 잘 모르지? 내가 같이 가줄게!", "RoRo"));

        DialogManager.Show(FirstDialog);
    }

    // private void SecondDialog()
    // {
    //     var SecondDialog = new List<DialogData>();

    //     SecondDialog.Add(new DialogData("[저기 하늘에 떠 다니는 건 뭐지?]", "User"));

    //     SecondDialog.Add(new DialogData("어디? 어디?", "NoonDung"));

    //     SecondDialog.Add(new DialogData("/wait:1/우리는! /wait:0.5/학교를 지키는 어벤져스, 눈꽃송이들이야!", "User"));

    //     SecondDialog.Add(new DialogData("/wait:0.5/안녕, 친구야! 혹시 무슨 고민 있어?", "NoonDung"));

    //     SecondDialog.Add(new DialogData("[(사정을 설명한다.)]", "User"));

    //     SecondDialog.Add(new DialogData("You can also change the character's sprite /emote:Sad/like this, /click//emote:Happy/Smile.", "Li"));

    //     SecondDialog.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Li"));

    //     SecondDialog.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li"));

    //     SecondDialog.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Li"));

    //     SecondDialog.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Li"));

    //     SecondDialog.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha."));

    //     SecondDialog.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Sa"));

    //     //DialogManager.Show(SecondDialog);
    // }

    
    private void Start()
    {
        FirstDialog();

    }
    
    private void Show_Animation(int index)
    {
        Animation[index].SetActive(true);
    }
}
