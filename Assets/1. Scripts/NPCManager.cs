using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OpenAI;
using System.Text;

public class NPCManager : MonoBehaviour
{
    [System.Serializable]
    public class NPC
    {
        public string name;
        public float attackSpeed;
        public float attackRange;

        public NPC(string name, float attackSpeed, float attackRange)
        {
            this.name = name;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

        public static NPC FromJSON(string json)
        {
            return JsonUtility.FromJson<NPC>(json);
        }
    }

    public NPC currentNPC;

    public void CreateNewNPC(string name)
    {
        currentNPC = new NPC(name, 1.0f, 1.0f);
    }

    public void SaveChatAsScript(List<ChatMessage> messages)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("");
        sb.AppendLine("public class NPC_Script : MonoBehaviour {");
        sb.AppendLine("\tpublic float attackSpeed = 1.0f;");
        sb.AppendLine("\tpublic float attackRange = 1.0f;");
        sb.AppendLine("");
        sb.AppendLine("\tpublic void ExecuteNPCBehavior() {");

        foreach (var message in messages)
        {
            if (message.Role == "system")
            {
                sb.AppendLine($"\t\t// {message.Content}");
            }
            else if (message.Role == "user")
            {
                if (message.Content.Contains("공격 속도 증가"))
                {
                    sb.AppendLine("\t\t// Increase Attack Speed");
                    sb.AppendLine("\t\tthis.attackSpeed += 0.1f;");
                }
                else if (message.Content.Contains("공격 범위 증가"))
                {
                    sb.AppendLine("\t\t// Increase Attack Range");
                    sb.AppendLine("\t\tthis.attackRange += 0.5f;");
                }
            }
        }

        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string path = "E:/dev/2DGPTGame/Assets/1. Scripts/NPCs/NPC_Script.cs";
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(sb.ToString());
        }
    }

    public void UpdateNPCAttributesFromChat(string chatOutput)
    {
        if (chatOutput.Contains("공격 속도 증가"))
        {
            currentNPC.attackSpeed += 0.1f;
        }
        if (chatOutput.Contains("공격 범위 증가"))
        {
            currentNPC.attackRange += 0.5f;
        }
    }

    public void SaveNPCToFile()
    {
        string path = "E:/dev/2DGPTGame/Assets/1. Scripts/NPCs/npc_" + currentNPC.name + ".json";
        string json = currentNPC.ToJSON();

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }
    }

    public void LoadNPCFromFile(string npcName)
    {
        string path = "E:/dev/2DGPTGame/Assets/1. Scripts/NPCs/npc_" + npcName + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            currentNPC = NPC.FromJSON(json);
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}