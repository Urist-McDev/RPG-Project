using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;
using RPG.Attributes;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using GameDevTV.Inventories;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        ActionStore actionStore;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float raycastRadius = 1f;
        [SerializeField] int numberOfAbilities = 6;

        bool isDraggingUI = false;

        private void Awake() 
        {
            health = GetComponent<Health>();
            ActionStore actionStore = GetComponent<ActionStore>();
        }

        private void Update()
        {
            if(InteractWithUI()) return;
           if (health.IsDead())
           {
                SetCursor(CursorType.None);
                return;
           }

           UseAbilities();

           if (InteractWithComponet()) return;
           if (InteractWithMovement()) return;

           SetCursor(CursorType.None);
        }

        private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    ActionStore actionStore = GetComponent<ActionStore>();
                    actionStore.Use(i, gameObject);
                }
            }
        }

        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }
                SetCursor(CursorType.UI);
                return true;
            }
            if (isDraggingUI)
            {
                return true;
            }
            return false;
        }

        private bool InteractWithComponet()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            foreach (RaycastHit hit in hits)
            {
                IRayCastable[] rayCastables = hit.transform.GetComponents<IRayCastable>();
                foreach (IRayCastable rayCastable in rayCastables)
                {
                    if (rayCastable.HandleRaycast(this))
                    {
                        SetCursor(rayCastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            return true;
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}