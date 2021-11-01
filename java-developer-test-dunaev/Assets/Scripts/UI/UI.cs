using Units;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    public class UI
    {
        public void SetBindings(UnityAction playAction, PlayerFabric playerFabric)
        {
            Object.FindObjectOfType<PlayButton>().GetComponent<Button>().onClick.AddListener(playAction);
            Object.FindObjectOfType<NextSkinButton>().GetComponent<Button>().onClick.AddListener(playerFabric.NextSkin);
            Object.FindObjectOfType<PrevSkinButton>().GetComponent<Button>().onClick.AddListener(playerFabric.PrevSkin);

            playerFabric.GetPlayer().ScoreUp += () => Object.FindObjectOfType<ScoreText>().Change?.Invoke(playerFabric.GetPlayer().Score);
        }
    }
}