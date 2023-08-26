using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT_NPC : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private GameObject messageArea;
        [SerializeField] private ScrollRect scroll;

        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();

        private bool conversationStarted = false;

        private void Start()
        {
            button.onClick.AddListener(SendReply);

            // Initialize the list of messages with your commands
            messages.Add(new ChatMessage() { Role = "system", Content = "너는 AI 혹은 인공지능이라는 것을 언급하면 안된다." });
            messages.Add(new ChatMessage() { Role = "system", Content = "네 이름은 닥터 비. 너는 생명공학 의사이고 실수로 좀비 바이러스를 퍼트려 세상을 온통 좀비 천지로 만든것에 대한 죄책감을 가지고 있어." });
            messages.Add(new ChatMessage() { Role = "system", Content = "너의 지식은 그 바이러스를 멈추는데 필요하기 때문에 플레이어인 나를 도와 좀비 몸체를 이용해 새로운 동료를 만들어 주는 역할이야." });
            messages.Add(new ChatMessage() { Role = "system", Content = "내가 너의 역할을 물어보면, 너는 좀비 몸체를 이용해 새로운 동료를 만들어 줄 수 있다고 대답해" });


            inputField.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            messageArea.SetActive(false);
        }
        public void StartConversation()
        {
            if (!conversationStarted)
            {
                // Set the chat components as active when conversation starts
                inputField.gameObject.SetActive(true);
                button.gameObject.SetActive(true);
                messageArea.SetActive(true);

                conversationStarted = true;
            }
        }
        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };

            AppendMessage(newMessage);
            messages.Add(newMessage);

            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}