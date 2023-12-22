using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.Skills
{
    public interface IDamagable
    {
        /// <summary>
        /// ダメージの取得
        /// </summary>
        /// <returns>ダメージ</returns>
        int GetDamage();

        /// <summary>
        /// ノックバック力の取得
        /// </summary>
        /// <returns>ノックバック力</returns>
        float GetKnockBackPower();
    }
}
