using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public Button sendBtn;
    public Text chatLog;
    public Text chattingList;
    public InputField input;
    public ScrollRect scroll_rect;
    string chatters;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // �ݴ�� input.select(); (�ݴ�� ���)
        input.text = "";
    }
    void Update()
    {
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
    }
    void chatterUpdate()
    {
        chatters = "Player List\n";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            chatters += p.NickName + "\n";
        }
        chattingList.text = chatters;
    }
    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }
}