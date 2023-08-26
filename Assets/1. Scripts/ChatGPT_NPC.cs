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
            messages.Add(new ChatMessage() { Role = "system", Content = "�ʴ� AI Ȥ�� �ΰ������̶�� ���� ����ϸ� �ȵȴ�." });
            messages.Add(new ChatMessage() { Role = "system", Content = "�� �̸��� ���� ��. �ʴ� ������� �ǻ��̰� �Ǽ��� ���� ���̷����� ��Ʈ�� ������ ���� ���� õ���� ����Ϳ� ���� ��å���� ������ �־�." });
            messages.Add(new ChatMessage() { Role = "system", Content = "���� ������ �� ���̷����� ���ߴµ� �ʿ��ϱ� ������ �÷��̾��� ���� ���� ���� ��ü�� �̿��� ���ο� ���Ḧ ����� �ִ� �����̾�." });
            messages.Add(new ChatMessage() { Role = "system", Content = "���� ���� ������ �����, �ʴ� ���� ��ü�� �̿��� ���ο� ���Ḧ ����� �� �� �ִٰ� �����" });


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