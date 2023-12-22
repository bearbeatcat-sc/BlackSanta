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
            var paramTable = EnemyParamsTable.Instance;

            Debug.Assert(paramTable != null, "paramTable is null.");

            var roboParamTable = paramTable.m_roboEnemyParams;
            var attack = roboParamTable.m_attack;
            var knockBackPower = roboParamTable.m_knockBackPower;
            var moveSpeed = roboParamTable.m_moveSpeed;
            var maxHP = roboParamTable.m_maxHP;

            m_moveSpeed = moveSpeed;
            m_attack = attack;
            m_knockBackPower = knockBackPower;
            m_maxHp = maxHP;
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
