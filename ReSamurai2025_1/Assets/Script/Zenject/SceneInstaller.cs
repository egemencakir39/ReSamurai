using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private AttackBarControllerUI _attackBarControllerUI;
    [SerializeField] private DragController _dragController;            
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SoundManager _soundManager;

    public override void InstallBindings()
    {
        Container.Bind<AttackBarControllerUI>().FromInstance(_attackBarControllerUI).AsSingle();
        Container.Bind<DragController>().FromInstance(_dragController).AsSingle();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle();
        Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
    }
}
