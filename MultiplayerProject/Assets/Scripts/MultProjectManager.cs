using System;
using Unity.Netcode;
using UnityEngine.UIElements;
using UnityEngine;


namespace MultProject
{
    public class MultProjectManager : MonoBehaviour
    {
        VisualElement rootVisualElement;
        Button hostButton;
        Button clientButton;
        Button serverButton;
        Button moveButton;
        Label statusLabel;


        //create buttons 
        void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            rootVisualElement = uiDocument.rootVisualElement;

            hostButton = CreateButton("HostButton", "Host");
            clientButton = CreateButton("ClientButton", "Client");
            serverButton = CreateButton("ServerButton", "Server");
            statusLabel = CreateLabel("StatusLabel", "Not Connected");

            rootVisualElement.Clear();
            rootVisualElement.Add(hostButton);
            rootVisualElement.Add(clientButton);
            rootVisualElement.Add(serverButton);
            rootVisualElement.Add(moveButton);
            rootVisualElement.Add(statusLabel);

            hostButton.clicked += OnHostButtonClicked;
            clientButton.clicked += OnClientButtonClicked;
            serverButton.clicked += OnServerButtonClicked;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateUI();
        }

        void OnDisable()
        {
            hostButton.clicked -= OnHostButtonClicked;
            clientButton.clicked -= OnClientButtonClicked;
            serverButton.clicked -= OnServerButtonClicked;
        }

        void OnHostButtonClicked() => NetworkManager.Singleton.StartHost();

        void OnClientButtonClicked() => NetworkManager.Singleton.StartClient();

        void OnServerButtonClicked() => NetworkManager.Singleton.StartServer();

        private Button CreateButton(string name, string text)
        {
            var button = new Button();
            button.name = name;
            button.text = text;
            button.style.width = 240;
            button.style.backgroundColor = Color.white;
            button.style.color = Color.black;
            button.style.unityFontStyleAndWeight = FontStyle.Bold;

            return button;
        }

        private Label CreateLabel(string name, string content)
        {
            var label = new Label();
            label.name = name;
            label.text = content;
            label.style.color = Color.black;
            label.style.fontSize = 18;

            return label;
        }

        void UpdateUI()
        {
            if(NetworkManager.Singleton == null)
            {
                SetStartButton(false);
                SetStatusText("NetworkManager not found");
                return;
            }

            if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                SetStartButton(true);
                SetStatusText("Not connected");

            }

            else
            {
                SetStartButton(false);
                UpdateStatusLabels();
            }
        }

        void SetStartButton(bool state)
        {
            hostButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
            clientButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
            serverButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void SetMoveButton(bool state)
        {
            moveButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;

            if(state)
            {
                moveButton.text = NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change";
            }
        }

        void SetStatusText(string text) => statusLabel.text = text;

        void UpdateStatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
            string transport = "Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name;
            string modeText = "Mode: " + mode;
            SetStatusText($"{transport}\n{modeText}");
        }
    }
}

