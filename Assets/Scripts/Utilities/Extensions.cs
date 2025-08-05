using UnityEngine;
using Unity.Netcode;
using UnityEngine.AI;

namespace Horror.Utilities
{
    public static class Extensions
    {
        public static void DrawGroundContactGizmos(this CharacterController controller)
        {
            //* Draws a sphere at the ground contact point to visualize if the character is grounded.
            float radius = controller.radius - controller.skinWidth;
            Vector3 position = controller.transform.position + Vector3.down * (controller.height * .5f - controller.radius + controller.skinWidth * 2f);
            Gizmos.color = controller.isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(position, radius);
        }

        public static bool CanControl(this NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.gameObject.activeInHierarchy && networkBehaviour.IsSpawned && networkBehaviour.IsOwner;
        }

        public static Transform FindTarget(this Transform transform)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            float closestPlayerDistance = Mathf.Infinity;
            Transform closestPlayer = null;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestPlayerDistance)
                {
                    closestPlayerDistance = distance;
                    closestPlayer = player.transform;
                }
            }

            return closestPlayer;
        }

        public static float CalculateDistance(this NavMeshPath path)
        {
            float distance = 0f;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return distance;
        }
    }
}
