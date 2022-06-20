using UnityEngine;
using UnityEngine.AI;

public static class AgentType
{
    /// <summary>
    /// This class was being used for determining the agent type of a npc which is different than humanoid.
    /// </summary>
    /// <param name="agentTypeName"></param>
    /// <returns></returns>
    public static int GetAgenTypeIDByName(string agentTypeName)
    {
        int count = NavMesh.GetSettingsCount();
        string[] agentTypeNames = new string[count + 2];
        for (var i = 0; i < count; i++)
        {
            int id = NavMesh.GetSettingsByIndex(i).agentTypeID;
            string name = NavMesh.GetSettingsNameFromID(id);
            if (name == agentTypeName)
            {
                return id;
            }
        }
        return -1;
    }
}
