using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatManager
{
    public class CheatActionDescription
    {
        public readonly string name;
        public readonly Action cheatAction;

        public CheatActionDescription(string name, Action cheatAction)
        {
            this.name = name;
            this.cheatAction = cheatAction;
        }
    }

    public class CheatElementBehaviour : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;

        public void Setup(CheatActionDescription description)
        {
            _text.text = description.name;
            _button.onClick.AddListener(() => description.cheatAction());
        }
    }

    public interface ICheatProvider
    {
        IEnumerable<CheatActionDescription> GetCheatActions();
    }

    public static readonly CheatManager Instance = new CheatManager();

    private readonly List<ICheatProvider> _providers = new List<ICheatProvider>();

    private GameObject _panelPrefab;
    private CheatElementBehaviour _cheatElementPrefab;

    private GameObject _panel;

    public void Setup(GameObject panelPrefab, CheatElementBehaviour cheatElementPrefab)
    {
        _panelPrefab = panelPrefab;
        _cheatElementPrefab = cheatElementPrefab;
    }

    public void RegProvider(ICheatProvider provider)
    {
        _providers.Add(provider);
    }

    public void ShowCheatPanel()
    {
        if (_panel != null)
            return;

        _panel = UnityEngine.Object.Instantiate(_panelPrefab);
        foreach (var provider in _providers)
        {
            foreach (var cheatAction in provider.GetCheatActions())
            {
                var element = UnityEngine.Object.Instantiate(_cheatElementPrefab, _panel.transform);

                element.Setup(cheatAction);
            }
        }
    }

    public void HideCheatPanel()
    {
        if (_panel == null)
            return;

        UnityEngine.Object.Destroy(_panel);
        _panel = null;
    }
}

public class SomeManagerWithCheats : CheatManager.ICheatProvider
{
    private int _health;

    public void Setup()
    {
        CheatManager.Instance.RegProvider(this);
    }

    IEnumerable<CheatManager.CheatActionDescription> CheatManager.ICheatProvider.GetCheatActions()
    {
        yield return new CheatManager.CheatActionDescription("Cheat health", () => _health++);
        yield return new CheatManager.CheatActionDescription("Reset health", () => _health = 0);
    }
}
