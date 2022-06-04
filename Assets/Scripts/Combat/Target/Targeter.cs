using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    [SerializeField] List<Target> targets = new List<Target>{};
    Camera _camera;
    public Target CurrentTarget;

    void Start() 
    {
        _camera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        
        targets.Remove(target);
        Cancel();
    }

    // FreelookState에서 타켓 키 눌렀을 때 실행
    public bool SelectTarget()
    {
        
        if (targets.Count == 0) return false;

        Target closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            // 타깃의 위치 값을 1, 1 사이즈 화면을 기준으로 return 그래서 가운데 좌표값이 0.5, 0.5
            Vector2 viewPos = _camera.WorldToViewportPoint(target.transform.position);

            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            
            if (toCenter.sqrMagnitude < closestDistance)
            {
                closestTarget = target;
                closestDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) return false;

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }


    // Cancel과 유사하지만 타겟 파괴 시 사용
    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }


}
