using DG.Tweening;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary
{
    public class DiaryCameraController : MonoBehaviour
    {
        public Transform camTrans;

        public Transform ref_default;
        public Transform ref_z_p;
        public Transform ref_z_n;
        public Transform ref_y_p;
        public Transform ref_y_n;

        public Transform ref_comedy;
        public Transform ref_focus;

        public float duration_long;
        public float duration_short;

        Action _callback;

        public void SetCallback(Action action)
        {
            _callback = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zPositive">取值为 -1 0 1</param>
        /// <param name="yPositive">取值为 -1 0 1</param>
        public void TurnTo(int zPositive, int yPositive, bool longDuration = true)
        {
            var t = GetTransform(zPositive, yPositive);
            camTrans.DOKill();

            var d = longDuration ? duration_long : duration_short;
            camTrans.DOMove(t.Item1, d);
            camTrans.DORotateQuaternion(t.Item2, d).OnComplete(() =>
            {
                _callback?.Invoke();
                _callback = null;
            });
        }

        public void TurnTo(Transform t, bool longDuration = true)
        {
            camTrans.DOKill();

            var d = longDuration ? duration_long : duration_short;
            camTrans.DOMove(t.position, d);
            camTrans.DORotateQuaternion(t.rotation, d).OnComplete(() =>
            {
                _callback?.Invoke();
                _callback = null;
            });
        }

        public (Vector3, Quaternion) GetTransform(int zPositive, int yPositive)
        {
            if (zPositive == 1 && yPositive == 1)
            {
                return ((ref_z_p.position + ref_y_p.position) * 0.6f, Quaternion.Lerp(ref_z_p.rotation, ref_y_p.rotation, 0.5f));
            }
            else if (zPositive == 1 && yPositive == -1)
            {
                return ((ref_z_p.position + ref_y_n.position) * 0.6f, Quaternion.Lerp(ref_z_p.rotation, ref_y_n.rotation, 0.5f));
            }
            else if (zPositive == -1 && yPositive == 1)
            {
                return ((ref_z_n.position + ref_y_p.position) * 0.6f, Quaternion.Lerp(ref_z_n.rotation, ref_y_p.rotation, 0.5f));
            }
            else if (zPositive == -1 && yPositive == -1)
            {
                return ((ref_z_n.position + ref_y_n.position) * 0.6f, Quaternion.Lerp(ref_z_n.rotation, ref_y_n.rotation, 0.5f));
            }
            else if (zPositive == 0 && yPositive == 1)
            {
                return (ref_y_p.position, ref_y_p.rotation);
            }
            else if (zPositive == 0 && yPositive == -1)
            {
                return (ref_y_n.position, ref_y_n.rotation);
            }
            else if (zPositive == 1 && yPositive == 0)
            {
                return (ref_z_p.position, ref_z_p.rotation);
            }
            else if (zPositive == -1 && yPositive == 0)
            {
                return (ref_z_n.position, ref_z_n.rotation);
            }

            return (ref_default.position, ref_default.rotation);
        }

    }
}