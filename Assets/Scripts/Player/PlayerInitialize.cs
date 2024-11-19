using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInitialize : MonoBehaviour
    {
        [SerializeField] private MeshFilter playerHeadMeshFilter;
        [SerializeField] private MeshRenderer playerHeadMeshRenderer;
        
        [SerializeField] private List<MeshFilter> chosenHeadMeshFilters;
        [SerializeField] private List<MeshRenderer> chosenHeadMeshRenderers;
        
        
        
        [SerializeField] private MeshFilter playerNoseMeshFilter;
        [SerializeField] private MeshRenderer playerNoseMeshRenderer;
        

        [SerializeField] private List<MeshFilter> chosenNoseMeshFilters;
        [SerializeField] private List<MeshRenderer> chosenNoseMeshRenderers;

        [SerializeField] private int chosenIndex;

        public int ChosenIndex
        {
            get => chosenIndex;
            set => chosenIndex = value;
        }

        private void Awake()
        {
            playerHeadMeshFilter.mesh = chosenHeadMeshFilters[ChosenIndex].sharedMesh;
            playerHeadMeshRenderer.material = chosenHeadMeshRenderers[ChosenIndex].sharedMaterial;

            playerNoseMeshFilter.mesh = chosenNoseMeshFilters[ChosenIndex].sharedMesh;
            playerNoseMeshRenderer.material = chosenNoseMeshRenderers[ChosenIndex].sharedMaterial;
        }
    }
}