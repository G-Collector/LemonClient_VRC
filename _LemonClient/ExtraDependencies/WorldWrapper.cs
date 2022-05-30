using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.SDKBase;
using VRC.Core;

namespace _LemonClient.ExtraDependencies
{
	internal static class WorldWrapper              //THANKS ZeroDay Remastered!!
	{
		internal static ApiWorld CurrentWorld()
		{
			return RoomManager.field_Internal_Static_ApiWorld_0;
		}


		internal static ApiWorldInstance CurrentInstance()
		{
			return RoomManager.field_Internal_Static_ApiWorldInstance_0;
		}


		internal static IEnumerable<Player> GetPlayers()
		{
			return PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
		}


		internal static bool IsInRoom()
		{
			return RoomManager.field_Internal_Static_ApiWorld_0 != null && RoomManager.field_Internal_Static_ApiWorldInstance_0 != null;
		}


		internal static int GetPlayerCount()
		{
			return PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.Count;
		}


		internal static string GetJoinID()
		{
			return WorldWrapper.CurrentInstance().id;
		}


		internal static void JoinWorld(string worldID)
		{
			Networking.GoToRoom(worldID);
		}
	}
}
