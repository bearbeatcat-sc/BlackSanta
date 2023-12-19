using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class RobotEnemy : BaseEnemy
    {
        /// <summary>
        /// 反転コンポーネント
        /// </summary>
        [SerializeField]
        private FlipComponent m_flipComponent = null;

        public override void Attack()
        {

        }

        public override void Death()
        {

        }

        public override void Initialize()
        {
            m_flipComponent.SetInitScale(transform.localScale);
        }

        public override void Move()
        {
            var playerPosition = GetPlayerPosition();
            if (playerPosition.magnitude == 0.0f) return;

            var transformPosiiton = transform.position;
            var currentPosition = new Vector2(transformPosiiton.x, transformPosiiton.y);
            var vec = playerPosition - currentPosition;
            vec.Normalize();

            currentPosition += vec * Time.deltaTime * m_moveSpeed;
            transform.position = new Vector3(currentPosition.x, currentPosition.y, 0);

            m_flipComponent.Flip(vec.x <= 0.0f);
        }
    }
}
