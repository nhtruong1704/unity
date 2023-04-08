using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] GameObject joinChat;
    [SerializeField] GameObject chatPanel;
    [SerializeField] GameObject connectingText;
    [SerializeField] GameObject messengerPrefab;
    [SerializeField] GameObject joinPrefab;
    [SerializeField] GameObject leftPrefab;
    [SerializeField] Transform contentTransform;
    [SerializeField] string userName;
    ChatClient chatClient;
    bool isConnected;

    string currentChat;
    [SerializeField] TMP_InputField chatField;
    void Update()
    {
        if(isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }
    }

    public void SubmitPublicChatOnClick()
    {
        if (chatField.text != "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void UserNameOnValueChange(string value)
    {
        userName = value;
    }

    public void ChatConnectOnClick()
    {
        connectingText.SetActive(true);
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
            PhotonNetwork.AppVersion, new AuthenticationValues(userName));
        Debug.Log("Connecting");
    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    public void ChatDisconnecOnClick()
    {
        chatClient.Unsubscribe(new string[] { "RegionChannel" });
        chatClient.Disconnect();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        if (state == ChatState.Uninitialized)
        {
            isConnected = false;
            joinChat.SetActive(true);
            chatPanel.SetActive(false);
        }
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
        connectingText.SetActive(false);
        joinChat.SetActive(false);
        chatClient.Subscribe("RegionChannel" ,0,-1, new ChannelCreationOptions() { PublishSubscribers = true});
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }   

    public void OnDisconnected()
    {
        print("Disconnected");
        isConnected = false;
        joinChat.SetActive(true);
        chatPanel.SetActive(false);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            Messenger.Create(messengerPrefab, senders[i], messages[i].ToString(), contentTransform, userName);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {
        Notification.Create(joinPrefab, user + " joined", contentTransform);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Notification.Create(leftPrefab, user + " left", contentTransform);
    }

}
