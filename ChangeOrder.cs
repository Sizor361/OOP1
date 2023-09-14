using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    interface ChangeOrder
    {
        /// <summary>
        /// Проверяем корректность ввода и записываем в БД
        /// </summary>
        void CheckAndWrite();

        /// <summary>
        /// Показываем в БД информацию об изменении записи
        /// </summary>
        void WriteChanges();

        /// <summary>
        /// Обновляем старую БД, нужна чтобы показывать что изменилось
        /// </summary>
        void FillOldOrders();
    }
}
